using System.Linq;
using FluentAssertions;
using Xunit;

namespace CsharpPoker.Tests
{
    public class HandTests
    {
        [Fact]
        public void CanCreateHand()
        {
            var hand = new Hand();
            hand.Cards.Any().Should().BeFalse();
        }

        [Fact]
        public void CanHandDrawCard()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);
            var hand = new Hand();

            hand.Draw(card);

            hand.Cards.First().Should().Be(card);
        }
    }
}
