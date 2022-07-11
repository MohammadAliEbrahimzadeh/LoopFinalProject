using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using LoopMainProject.Common.Helpers;
using Shop.Application.Extentions;

namespace LoopMainProject.Test
{
    public class PasswordHelperTest
    {
        [Theory]
        [MemberData(nameof(PasswordData))]
        public async Task PasswordHelperWorkCorrectly(string plainText, string correctEncryptedText)
        {
            var encryptedText = await plainText.GetHashStringAsync();

            encryptedText.Should().Be(correctEncryptedText);
        }

        public static IEnumerable<object[]> PasswordData() =>
            new List<object[]>
            {
            new object[]
            {
                "admin",
                "c7ad44cbad762a5da0a452f9e854fdc1e0e7a52a38015f23f3eab1d80b931dd472634dfac71cd34ebc35d16ab7fb8a90c81f975113d6c7538dc69dd8de9077ec"
            },
            new object[]
            {
                "mohammad1998",
                "6327c2419ef2f5e705f18256851199dc2e02408688d376672b890ffbc07a618d28fee7144e13a30fb2c7a71c130cce7cdfb2756f10b245cd145fe81adebbe18f"
            }
            };


        [Theory]
        [MemberData(nameof(SaltPasswordData))]
        public async Task SaltPasswordHelperWorkCorrectly(string plainText, string correctEncryptedText)
        {
            var encryptedText = HashGenerator.GeneratePassword(plainText);

            encryptedText.Should().Be(correctEncryptedText);
        }

        public static IEnumerable<object[]> SaltPasswordData() =>
            new List<object[]>
            {
            new object[]
            {
                "mohammad1998",
                "WQVWYXSIh6BpGApHCOVQ0tZqXKj4gapMhMxQdJ1FQuToZ6KjQs4I6YGF9FmjtGBzgPDN3kyua56Ob/nucXyRx29T5FGDw=="
            }
            };
    }
}