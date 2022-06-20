using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Event.Web.Authorization;
using NUnit.Framework;

namespace Event.Web.Test.Authorization
{
    public class IdentityExtensionTest
    {
        [Test]
        public void TestUserIdExtensionExist()
        {
            var expectedId = 1234;
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimNameConstants.UserId, expectedId.ToString()));

            Assert.AreEqual(expectedId, claims.UserId(), "UserId is missing in the claims");
        }

        [Test]
        public void TestUserIdExtensionMissing()
        {
            var expectedMessage = $"Claim type: '{ClaimNameConstants.UserId}' can't be found";
            var expectedId = 1234;
            var claims = new List<Claim>();
            claims.Add(new Claim("Not UserID", expectedId.ToString()));
            try
            {
                claims.UserId();
                Assert.Fail("UserId is not missing in the claims");
            }
            catch (KeyNotFoundException e)
            {
                Assert.AreEqual(expectedMessage, e.Message);
            }
        }
    }
}
