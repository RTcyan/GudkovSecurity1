
using GudkovSecurity1;
using GudkovSecurity1.models;
using System;
using System.IO;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact(DisplayName = "7 раз логин по неверному паролю и 1 раз по верному паролю")]
        async public void Test1()
        {
            var repository = RepositoryService.getInstance();
            int nullUserCount = 0;
            int blockedUserExeptionsCount = 0;

            int numberOfTimes = 7;

            for (int i = 0; i < numberOfTimes; i++)
            {
                try
                {
                    User user = await repository.login("user1", GetRandomAlphaNumeric());
                    if (user == null)
                    {
                        nullUserCount++;
                    }
                } catch (Exception ex)
                {
                    if (typeof(UserWasBlockedException).IsInstanceOfType(ex))
                    {
                        blockedUserExeptionsCount++;
                    }
                }
            }
            var lastBlockedUserExceptionWasFailure = false;

            try
            {
                await repository.login("user1", "user1");
            } catch (Exception ex)
            {
                if (typeof(UserWasBlockedException).IsInstanceOfType(ex))
                {
                    lastBlockedUserExceptionWasFailure = true;
                }
            }

            int expectedBlockedUserCount = (int)Math.Floor((double)numberOfTimes / 3);
            int expectedNullUserCount = numberOfTimes - expectedBlockedUserCount;

            Assert.True(lastBlockedUserExceptionWasFailure);
            Assert.Equal(expectedNullUserCount, nullUserCount);
            Assert.Equal(expectedBlockedUserCount, blockedUserExeptionsCount);
        }

        [Fact(DisplayName = "Логин по пустому логину и паролю")]
        async public void Test2()
        {
            var repository = RepositoryService.getInstance();
            User user = await repository.login("", "");

            Assert.Null(user);
        }

        [Fact(DisplayName = "Смена пароля на пустое значение")]
        async public void Test3()
        {
            var repository = RepositoryService.getInstance();
            var exText = "";
            try
            {
                await repository.updatePassword("", GetRandomAlphaNumeric());
            } catch (Exception ex)
            {
                exText = ex.Message;
            }

            var expectedExText = "Новый пароль должен состоять минимум из 6 символов. Должны присутствовать латинские символы с нижнем и верхнем регистре и цифры";

            Assert.Equal(exText, expectedExText);
        }

        [Fact(DisplayName = "Блокировка несуществующего пользователя")]
        async public void Test4()
        {
            var repository = RepositoryService.getInstance();
            var exText = "";
            var userLogin = "MY_TEST_USER_LOGIN";
            try
            {
                await repository.blockOrUnblockUser(new User { Id = 112321312, isAdmin = false, isBlocked = false, LastBlockTime = null, Login = userLogin }) ;
            }
            catch (Exception ex)
            {
                exText = ex.Message;
            }

            var expectedExText = "Пользователя " + userLogin + " не найдено";

            Assert.Equal(exText, expectedExText);
        }

        [Fact(DisplayName = "Добавление поста с пустыми значениями")]
        async public void Test5()
        {
            var repository = RepositoryService.getInstance();

            _ = Assert.ThrowsAsync<ArgumentNullException>(async () =>
              {
                  await repository.addRow("", GetRandomAlphaNumeric());
              });
            _ = Assert.ThrowsAsync<ArgumentNullException>(async () =>
              {
                  await repository.addRow(GetRandomAlphaNumeric(), "");
              });
        }

        public static string GetRandomAlphaNumeric()
        {
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
        }
    }
}
