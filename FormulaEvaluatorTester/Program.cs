using FormulaEvaluator;
/// <summary>
/// Author: Kyle G. Gayliyev
/// Partner: N/A
/// Date: 01/17/2023
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Kyle G. Gayliyev - This work may not
/// be copied for use in Academic Coursework.
///
/// I, Kyle G. Gayliyev, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source. All
/// references used in the completion of the assignments are cited
/// in my README file.
///
/// <u>File Contents</u>
/// 
/// This is the "FormulaEvaluatorTester" file. It's a Consolve App. It tests methods and 
/// functions in the "FormulaEvaluator" class library. 
/// 
/// <u>Tests</u>
///  
/// ** Class Evaluator:**
/// 
///     * Lookup delegate function: It looks up the value 
///     of an expression when the expression is a variable. It returns 
///     the final result of the expression as an integer value.
///     
///     Example: the X1 is a variable has a value of 5. If the expression
///     is "2+X1", the look up function should return 5, hence the final
///     result of the expression will be 2 + 5 = 7.
///     
///     * Testing "Evaluate" function: Evaluates a mathimatical infix expression.
///     It is assumed that the expression has been broken into a sequence of tokens and
///     it will not be used with an expression that would cause an integer to overflow.
///     
///     Legal tokens:
///         -   the four operator symbols( + - * /),
///         -   left and right parentheses (, ),
///         -   non-negative integers,
///         -   whitespace, and 
///         -   variables consisting of one or more letters followed by one or more 
///         digits (like "A1" or "a1"). Letters can be lowercase or uppercase.
///      
///     Examples: 
///        -  if the input is "(2 + 3) * 5 + 2", the result should be 27. 
///        -  X1 is a variable. If the input is "(2 +X1) * 5 + 2", the result will
///           depends on the value of the variable X1. 
///        -  if X1 is 7, for example, the result should be 47.
///        
///    Any illegal expression(other than an empty string), should throw an exception during the testing.
///        
/// </summary>
/// 

//Intro
Console.WriteLine("Hello, World!\nWelcome to Kyle G. Gayliyev's FormulaEvaluatorTester pannel!\n\n");

//Tests
try
{
    //Basic/Simple tests using each of the 4 operators ( + - * /):
    Console.WriteLine("A) Basic tests using each of the 4 operators ( + - * /):");
    if (Evaluator.Evaluate("5+5", null) == 10) Console.WriteLine("5 + 5 =10");
    if (Evaluator.Evaluate("5-5", null) == 0) Console.WriteLine("5 - 5 =0");
    if (Evaluator.Evaluate("5/5", null) == 1) Console.WriteLine("5 / 5 =1");
    if (Evaluator.Evaluate("5*5", null) == 25) Console.WriteLine("5 * 5 =25\n");

    if (Evaluator.Evaluate("0+0", null) == 0) Console.WriteLine("0 + 0 =0");
    if (Evaluator.Evaluate("2-1", null) == 1) Console.WriteLine("2 - 1 =1");
    if (Evaluator.Evaluate("9*7", null) == 63) Console.WriteLine("9 * 7 =63");
    if (Evaluator.Evaluate("20/4", null) == 5) Console.WriteLine("20 / 4 =5\n");

    //Tests with longer expressions:
    Console.WriteLine("B) Tests using longer expressions:");
    if (Evaluator.Evaluate("5 + 5 * 2", null) == 15) Console.WriteLine("5 + 5 * 2 =15");
    if (Evaluator.Evaluate("5 * 5 + 2", null) == 27) Console.WriteLine("5 * 5 + 2 =27");
    if (Evaluator.Evaluate("(2 + 3) * 5 + 2", null) == 27) Console.WriteLine("(2 + 3) * 5 + 2 =27");

    if (Evaluator.Evaluate("4 * 5 + 6 /2 ", null) == 23) Console.WriteLine("4 * 5 + 6 / 2 = 23");
    if (Evaluator.Evaluate("12+ 8 * 2-(5-4) ", null) == 27) Console.WriteLine("12+ 8 * 2-(5-4) = 27");
    if (Evaluator.Evaluate("5 + (4 - 3) * 9 +1 ", null) == 14) Console.WriteLine("5 + (4 − 3) * 9 + 1 = 15");
    if (Evaluator.Evaluate("4- 4 * (10 - 9)", null) == 0) Console.WriteLine("4- 4 * (10 - 9)=0");
    if (Evaluator.Evaluate("5 + 7 * 4 - (11 + 6)", null) == 16) Console.WriteLine("5 + 7 * 4 - (11 + 6) =16");
    if (Evaluator.Evaluate("12 + (11 * 5 - 5) + 10", null) == 72) Console.WriteLine("12 + (11 * 5 - 5) + 10 =72");
    if (Evaluator.Evaluate("(7 + 3 * 3) * 10 + 11-1", null) == 170) Console.WriteLine("(7 + 3 * 3) * 10 + 11-1 =170");
    if (Evaluator.Evaluate("2 * 2 + 11 * 4 - (10 + 6)+1", null) == 33) Console.WriteLine("2 * 2 + 11 * 4 - (10 + 6)+1 =33");
    if (Evaluator.Evaluate("2 * (2 + 11) - 4 - (10 + 6)+1", null) == 7) Console.WriteLine("2 * 2 + 11 * 4 - (10 + 6)+1 =7");
    if (Evaluator.Evaluate("3 + (3 + 8) * 9", null) == 102) Console.WriteLine("3 + (3 + 8) * 9 =102");
    if (Evaluator.Evaluate("11 - (4 * 9 - 10)", null) == -15) Console.WriteLine("11 - (4 * 9 - 10) =-15");
    if (Evaluator.Evaluate("(12 - 7 * 2) -8", null) == -10) Console.WriteLine("(12 - 7 * 2) -8 =-10");

    //Long empty space
    if (Evaluator.Evaluate("(12 - 7 *                1)", null) == 5) Console.WriteLine("(12 - 7 *                1) =5");

    if (Evaluator.Evaluate("(2 - 10 * 6) - 7", null) == -65) Console.WriteLine("(2 - 10 * 6) - 7 =-65");
    if (Evaluator.Evaluate("(11 * (11 - 12) * 11 - 9+(50+25+25) ) /2", null) == -15) Console.WriteLine("(11 * (11 - 12) * 11 - 9+(50+25+25) ) /2 =-15");
    if (Evaluator.Evaluate("4 * 6 + 3 - (4 + 3)", null) == 20) Console.WriteLine("4 * 6 + 3 − (4 + 3) =20\n");

    //Testing expressions with variables:
    Console.WriteLine("C) Testing expressions with variables:");
    Console.WriteLine("When variable A1=0, expression = 4 * 6 + 3 - (4 + 3) + A1 =" + Evaluator.Evaluate("4 * 6 + 3 - (4 + 3) + A1", (A1) => 0));
    Console.WriteLine("When variable z841=5, expression = 4 * 6 + 3 - (4 + 3) + z841 =" + Evaluator.Evaluate("4 * 6 + 3 - (4 + 3) + z841", (z841) => 5));

    Console.WriteLine("When variable AA11=10, expression = 4 * 6+ AA11 + 3 - (4 + 3) =" + Evaluator.Evaluate("4 * 6 + AA11+3 - (4 + 3) ", (AA11) => 10));
    Console.WriteLine("When variable zzz44=20, expression = 4 * 6 + 3 - (4 + 3)*zzz44 =" + Evaluator.Evaluate("4 * 6 +3 - (4 + 3)*zzz44 ", (zzz44) => 20));
    Console.WriteLine("When variable K7=-4, expression = 4 * 6 + 3 -K7* (4 + 3) =" + Evaluator.Evaluate("4 * 6 +3 - K7 * (4 + 3) ", (K7) => -4));
    Console.WriteLine("When variable K7=2; MM2=-7, expression = 4 * 6 + 3*( MM2+2) -K7* (4 + 3) =" + Evaluator.Evaluate("4 * 6 +3 *(MM2 + 2) - K7 * (4 + 3) ", getvariableValues) + "\n");

    //Exception catching tests. Tests are commented out because
    //the program gets stuck when it catches the exception. 
    //They can be uncommented and tested.

    Console.WriteLine("D) Exception catching tests (Please check the test \nfile for more commented out tests):");
    //  Evaluator.Evaluate("4 * 6 + 3 /0 + A1", (A1) => 0); //devided by zero
    //  Evaluator.Evaluate("4 * 6  + A1A", (A1A) => 0); //wrong variable
    //  Evaluator.Evaluate("4 * 6* + A1", (A1) => 0); //wrong expression with extra operator
    //  Evaluator.Evaluate("4 * 6)  + A1", (A1) => 0); //wrong expression with extra closed-parentheses
    //  Evaluator.Evaluate("4 * 6(  + A1", (A1) => 0); //wrong expression with extra open-parentheses
    //  Evaluator.Evaluate("4 * 6  + A1)", (A1) => 0); //wrong expression with extra closed-parentheses in the end
    //  Evaluator.Evaluate("4 * 6  + 3*(A1", (A1) => 0); //wrong expression with extra closed-parentheses in the middle
    //  Evaluator.Evaluate("4 * 6 3 + 3*A1", (A1) => 0); //wrong expression. Two numbers with a space in between
    //  Evaluator.Evaluate("4 * 6  + 3*(2+7)3", (A1) => 0); //wrong expression. Two numbers in the end with a space in between 
    //  Evaluator.Evaluate("4 * 6*((  + 3*(2+7)", (A1) => 0); //wrong expression with extra 2 open-parentheses
    //  Evaluator.Evaluate("4 * 6  + 3*(2+7)))", (A1) => 0); //wrong expression with extra 2 closed-parentheses
    //  Evaluator.Evaluate(null, null); //wrong expression , passes null.


    //TESTS END.

    Console.WriteLine("\n");

    /// <summary>
    /// Function to look up variable values.
    /// Receives a string parameter, which is the variable, and returns it's 
    /// associated value as an integer.
    /// 
    /// 
    /// If the associated/searched value of the variable is not found, it'll throw an ArgumentException.
    ///     
    /// </summary>
    /// <param name="variable"> a string variable to look up a value</param>
    /// <returns> Returns an integer value of the variable, or throws an ArgumentException value of the variable is not found</returns>
    int getvariableValues(string variable)
    {
        if (variable == "K7")
            return 2;
        else if (variable == "MM2")
            return -7;
        else
        {
            throw new ArgumentException("Variable has no  value!");

        }
    }
}
catch (DivideByZeroException e)
{
    Console.WriteLine(e + "\n");
}
catch (ArgumentException e)
{
    Console.WriteLine(e + "\n");
}

finally
{
    Console.WriteLine("Press enter to close...");
    Console.ReadLine();

}

