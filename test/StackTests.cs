using System;
using Xunit;


namespace CraftingCode
{
    public class StackTests
    {
        [Fact]
        public void PopShouldThrowWhenNoItems()
        {
            //Given
            var stack = new MyStack();
            //When
            //Then
            Assert.Throws<InvalidOperationException>(()=> stack.Pop());
        }
    }
}