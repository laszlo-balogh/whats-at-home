using System;
using System.Collections.Generic;
using System.Text;

namespace Api.IntegrationTests
{
    public static class TestData
    {
        public const string TestPassword = "Password123!";

        public static string GenerateRandomEmail()
        {
            string guid = Guid.NewGuid().ToString();
            string randomEmail = $"test_{guid}@example.com";
            return randomEmail;
        }
    }
}
