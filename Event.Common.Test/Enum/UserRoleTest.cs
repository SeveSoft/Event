using System;
using System.Linq;
using Event.Common.Enum;
using NUnit.Framework;

namespace Event.Common.Test.Enum
{
    public class UserRoleTest
    {
        [Test]
        public void TestUserRoleCount()
        {
            var expectedCount = 3;
            var names = System.Enum.GetNames(typeof(UserRole));

            Assert.AreEqual(expectedCount, names.Length, "Wrong number of enums");
        }

        [Test]
        public void TestUserRoleValues()
        {
            var names = System.Enum.GetNames(typeof(UserRole)).ToList();
            
            Assert.True(names.Contains("Admin"), "RoleUser: Admin is missing");
            Assert.True(names.Contains("Speaker"), "RoleUser: Speaker is missing");
            Assert.True(names.Contains("Participant"), "RoleUser: Participant is missing");
        }
    }
}
