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
            Assert.Throws<InvalidOperationException>(() => stack.Pop());

            stack.Push(new object());
            stack.Pop();
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Fact]
        public void PopReturnsTheLastPushedThing()
        {
            var stack = new MyStack();

            stack.Push(new object());
            stack.Push(null);

            object expected = new object();
            stack.Push(expected);

            object actual = stack.Pop();
            Assert.Same(expected, actual);
        }
    }

}