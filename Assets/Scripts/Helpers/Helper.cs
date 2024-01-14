using System.Collections.Generic;
/// <summary>
/// Helper Class 
/// </summary>
/// <typeparam name="T">Type</typeparam>
public static class Helper<T>
{
    public static Stack<T> CopyStack(Stack<T> originalStack)
    {
        Stack<T> tempStack = new Stack<T>(originalStack);
        Stack<T> copiedStack = new Stack<T>();

        while (tempStack.Count > 0)
        {
            copiedStack.Push(tempStack.Pop());
        }
        return copiedStack;
    }
}