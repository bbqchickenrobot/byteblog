﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Byte.Blog.Framework.Web;
using MvcContrib.PortableAreas;
using Xunit;

namespace Byte.Blog.Framework.UnitTests.Web
{
    public class AreaRegistrationBaseTests
    {
        [Fact]
        public void Invalid_context_state_throws_exception()
        {
            var area = new TestableAreaRegistration();

            var registrationContext = new AreaRegistrationContext("foo", new RouteCollection());

            Assert.Throws(
                typeof(Exception),
                () => area.RegisterArea(registrationContext, null));
        }

        [Fact]
        public void Default_area_name_is_set_to_current_assembly_namespace()
        {
            var area = new TestableAreaRegistration();

            Assert.Equal(this.GetType().Namespace, area.AreaName);
        }

        private class TestableAreaRegistration : AreaRegistrationBase
        {
            public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
            {
                typeof(AreaRegistrationContext)
                        .GetProperty("State")
                        .SetValue(context, new object(), null);

                base.RegisterArea(context, bus);
            }
        }
    }
}
