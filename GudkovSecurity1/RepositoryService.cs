using GudkovSecurity1.models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GudkovSecurity1
{
    public class RepositoryService
    {
        private string connString = "Host=localhost;Username=postgres;Password=postgres;Database=Security";

        private NpgsqlConnection conn;

        public User loginedUser;

        private static RepositoryService instance;

        private const int MAX_MISTAKES_COUNT = 3;

        private int mistakesCount = 0;

        private const string PASSWORD_PATTERN = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$";

        public static RepositoryService getInstance()
        {
            if (instance == null)
            {
                instance = new RepositoryService();
            }
            return instance;
        }

        public RepositoryService() {
            conn = new NpgsqlConnection(connString);
            conn.Open();
        }

        async public Task<User> getUser(string login, string password)
        {
            await using var cmd = new NpgsqlCommand("SELECT login, is_admin, id, last_block_datetime, is_blocked FROM users WHERE login = ($1) AND password = ($2)", conn)
            {
                Parameters =
                {
                    new() { Value = login },
                    new() { Value = password }
                }
            };
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    string userLogin = reader.GetString(0);
                    bool isAdmin = reader.GetBoolean(1);
                    int id = reader.GetInt32(2);
                    DateTime? lastBlockDateTime = reader.GetDateTime(3);
                    bool isBlocked = reader.GetBoolean(4);
                    User user = new User() { Login = userLogin, isAdmin = isAdmin, Id = id, LastBlockTime = lastBlockDateTime, isBlocked = isBlocked };
                    return user;
                }
            }
            return null;
        }

        async public Task<User> login(string login, string password)
        {
            User user = await getUser(login, password);
            if (user == null)
            {
                mistakesCount++;
                if (mistakesCount == MAX_MISTAKES_COUNT)
                {
                    setLastBlockTime(login, DateTime.Now);
                    mistakesCount = 0;
                    throw new UserWasBlockedException("Вход заблокирован на 1 минуту");
                }
                return null;
            }
            if (user.isBlocked)
            {
                throw new UserWasBlockedException("Пользователь был заблокирован администратором");
            }
            if (user.LastBlockTime.HasValue && user.LastBlockTime.Value.AddMinutes(1) >= DateTime.Now)
            {
                throw new UserWasBlockedException("Пользователь заблокирован, попробуйте еще раз через " + (60 - DateTime.Now.Subtract(user.LastBlockTime.Value).Seconds) + " секунд(ы)");
            }
            loginedUser = user;
            return user;
        }

        async public void setLastBlockTime(string login, DateTime lastBlockTime)
        {
            await using var cmd = new NpgsqlCommand("UPDATE users SET last_block_datetime = ($1) WHERE login = ($2)", conn)
            {
                Parameters =
                {
                    new() {Value = lastBlockTime},
                    new() {Value = login},
                }
            };
            cmd.ExecuteNonQuery();
        }

        async public Task<List<Post>> getData()
        {
            await using var cmd = new NpgsqlCommand("SELECT header, text, id FROM posts", conn);
            List<Post> postList = new List<Post>();
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Post post = new Post() { Header = reader[0].ToString(), Text = reader[1].ToString(), Id = int.Parse(reader[2].ToString()) };
                    postList.Add(post);
                }
            }
            return postList;
        }

        async public void deleteRow(int id)
        {
            await using var cmd = new NpgsqlCommand("DELETE FROM posts WHERE id = ($1)", conn)
            {
                Parameters =
                {
                    new() { Value = id },
                }
            };
            cmd.ExecuteNonQuery();
        }

        async public Task<bool> addRow(string header, string text)
        {
            if (header == "")
            {
                throw new ArgumentNullException();
            }
            if (text == "")
            {
                throw new ArgumentNullException();
            }
            await using var cmd = new NpgsqlCommand("INSERT INTO posts (header, text) VALUES (($1), ($2))", conn)
            {
                Parameters =
                {
                    new() {Value = header},
                    new() {Value = text}
                }
            };
            cmd.ExecuteNonQuery();
            return true;
        }

        async public void updatePost(Post post)
        {
            await using var cmd = new NpgsqlCommand("UPDATE posts SET header = ($1), text = ($2) WHERE id = ($3)", conn)
            {
                Parameters =
                {
                    new() {Value = post.Header},
                    new() {Value = post.Text},
                    new() {Value = post.Id}
                }
            };
            cmd.ExecuteNonQuery();
        }

        async public Task<bool> updatePassword(string newPassword, string oldPassword)
        {
            if (!Regex.IsMatch(newPassword, PASSWORD_PATTERN))
            {
                throw new UncorrectPasswordException("Новый пароль должен состоять минимум из 6 символов. Должны присутствовать латинские символы с нижнем и верхнем регистре и цифры");
            }

            User user = await getUser(loginedUser.Login, oldPassword);
            if (user == null)
            {
                throw new UncorrectPasswordException("Введенный пароль неверный");
            }

            await using var cmd = new NpgsqlCommand("UPDATE users SET password = ($1) WHERE id = ($2)", conn)
            {
                Parameters =
                {
                    new() {Value = newPassword},
                    new() {Value = loginedUser.Id},
                }
            };
            cmd.ExecuteNonQuery();

            return true;
        }

        async public Task<bool> blockOrUnblockUser(User user)
        {
            var users = await getUsers();
            var updatedUser = users.Find(x => x.Id == user.Id);
            if (updatedUser == null)
            {
                throw new UserNotFoundException("Пользователя " + user.Login + " не найдено");
            }
            await using var cmd = new NpgsqlCommand("UPDATE users SET is_blocked = ($1) WHERE id = ($2)", conn)
            {
                Parameters =
                {
                    new() {Value = user.isBlocked},
                    new() {Value = user.Id},
                }
            };
            cmd.ExecuteNonQuery();
            return true;
        }

        async public Task<List<User>> getUsers()
        {
            await using var cmd = new NpgsqlCommand("SELECT login, id, is_blocked FROM users WHERE is_admin = false", conn);

            List<User> users = new List<User>();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    User user = new User() { Login = reader[0].ToString(), Id = int.Parse(reader[1].ToString()), isBlocked = bool.Parse(reader[2].ToString()) };
                    users.Add(user);
                }
                reader.Close();
            }
            return users;
        }
    }

    public class UncorrectPasswordException : Exception
    {
        public UncorrectPasswordException() { }

        public UncorrectPasswordException(string message) : base(message) { }
    }

    public class UserWasBlockedException : Exception
    {

        public UserWasBlockedException() { }

        public UserWasBlockedException(string message): base(message) { }
    }

    public class UserNotFoundException : Exception
    {

        public UserNotFoundException() { }

        public UserNotFoundException(string message) : base(message) { }
    }
}
