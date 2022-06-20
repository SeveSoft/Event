using Event.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using NUnit.Framework;

namespace Event.Web.Test.Controllers
{
    public class AccountControllerTest
    {
        [Test]
        public void TestAllowAnonymousAttributeExistOnLoginMethod()
        {
            var attributes = new AccountController(null).GetAttributesOnMethod("Login");
            Assert.IsNotNull(attributes.FindAttribute<AllowAnonymousAttribute>(), "No AllowAnonymousAttribute found on Login() method");
        }

        [Test]
        public void TestAllowAnonymousAttributeExistOnSignUpLoginMethod()
        {
            var attributes = new AccountController(null).GetAttributesOnMethod("SignUpLogin");
            Assert.IsNotNull(attributes.FindAttribute<AllowAnonymousAttribute>(), "No AllowAnonymousAttribute found on SignUpLogin() method");
        }
        [Test]
        public void TestAuthorizeAttributeExistOnProfileMethod()
        {
            var attributes = new AccountController(null).GetAttributesOnMethod("Profile");
            Assert.IsNotNull(attributes.FindAttribute<AuthorizeAttribute>(), "No AuthorizeAttribute found on Profile() method");
        }

        [Test]
        public void TestAuthorizeAttributeExistOnLogoutMethod()
        {
            var attributes = new AccountController(null).GetAttributesOnMethod("Logout");
            Assert.IsNotNull(attributes.FindAttribute<AuthorizeAttribute>(), "No AuthorizeAttribute found on Logout() method");
        }

        [Test]
        public void TestAuthorizeAttributeExistOnAccessDeniedMethod()
        {
            var attributes = new AccountController(null).GetAttributesOnMethod("AccessDenied");
            Assert.IsNotNull(attributes.FindAttribute<AuthorizeAttribute>(), "No AuthorizeAttribute found on AccessDenied() method");
        }

    }
}
