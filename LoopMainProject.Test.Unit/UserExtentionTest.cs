using FluentAssertions;
using LoopMainProject.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoopMainProject.Test.Unit
{
    public class UserExtentionTest
    {
        [Theory]
        [MemberData(nameof(UserExtentionTestData))]
        public async Task GetUserId_Test(string CorrentUserId)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            var userId = claimsPrincipal.GetUserId();

            userId.Should().Be(CorrentUserId);

        }


        public static IEnumerable<object[]> UserExtentionTestData() =>

            new List<object[]>
            {
                new object[]
                {
                  "1"
                }
            };
    }
}

