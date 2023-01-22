using System.Diagnostics;
using System.Text.RegularExpressions;
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
/// **Class Evaluator:**
///     * Lookup: It's a delegate function that looks up the value of
///     an expression when the expression is a variable. It returns 
///     the final result of the expression as an integer value.
///     
///     * Evaluate function: Evaluates a mathematical infix expression.
///     It is assumed that the expression is broken into a sequence of tokens and
///     it will not be used with an expression that would cause an integer to overflow.
///     
///     Legal tokens:
///         -   the four operator symbols( + - * /),
///         -   left and right parentheses (, ),
///         -   non-negative integers (only positive integers),
///         -   whitespace, and 
///         -   variables consisting of one or more letters followed by one or more 
///         digits (like "A1", "a2", "AA33", or "aa4"). Letters can be lowercase or uppercase.
///      
///     Examples: 
///        -  if the input is "(2 + 3) * 5 + 2", the result should be 27. 
///        -  X1 is a variable. If the input is "(2 +X1) * 5 + 2", the result will
///           depends on the value of the variable X1. 
///        -  if X1 is 7, for example, the result should be 47.
///        
///     If the method detects any other kind of token(other than an empty string),
///     it'll throw an ArgumentException.
///        
/// </summary>
namespace FormulaEvaluator
{
    public static class Evaluator
    {
        /// <summary>
        /// Lookup is a delegate function. It looks up the value of a variable. 
        /// Given a variable name as its parameter, it will either return 
        /// an integer (the value of the variable) 
        /// or throw an ArgumentException if the variable has no value.
        /// </summary>
        /// <param name="varName"> a string parameter for the method. A variable to look up it's value.</param>
        /// <returns>Returns either the value of the variable as an integer, or throws ArgumentException if the variable has no value.</returns>
        public delegate int Lookup(string varName);


        /// <summary>
        /// Evaluates a mathematical infix expression. It returns the final result 
        /// of the expression as an integer value. It is assumed that the 
        /// expression has been broken into a sequence of tokens and it will not be used 
        /// with an expression that would cause an integer to overflow. 
        /// 
        /// Upon an "expression" is rendered to the method, it uses Regular Expressions
        /// to split the string into tokens. For example, if expression = "(2+35)*A7" 
        /// (expression is in a string type), it will return "["(", "2", "+", "35", ")", "*", "A7"]"
        /// in an array format.
        /// 
        /// Empty strings, leading and trailing whitespaces in each token will be ignored.
        /// 
        ///     Legal tokens:
        ///             -   the four operator symbols( + - * /),
        ///             -   left and right parentheses,
        ///             -   non-negative integers,
        ///             -   whitespace, and 
        ///             -   variables consisting of one or more letters followed by one or more digits.
        ///             Letters can be lowercase or uppercase.
        /// 
        /// The minus sign should not be used as a "unary" operation. For example,
        /// these are illegal: "-5" or "-(5/5)".
        /// 
        /// If the method detects any  other kind of token(other than an empty string),
        ///     it'll throw an ArgumentException.
        ///     
        /// </summary>
        /// <param name="expression"> a string variable that is expected to be a mathematical infix</param>
        /// <param name="variableEvaluator"> A variable holding an integer value or mathematical expression</param>
        /// <returns> Returns the final result of the expression as an integer value, or throws an ArgumentException for illgal tokens</returns>
        public static int Evaluate(string expression, Lookup variableEvaluator)
        {
            int numInteger;
            char operatorChar;

            //Splits the expression string into tokens using regular expressions
            //The regular expression code is taken from the
            //assignment pdf that was provided by the class instructor.
            string[] expressionSplitIntoTokensArray = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            Stack<int> valueStack = new Stack<int>();
            Stack<char> operatorStack = new Stack<char>();

            foreach (string token in expressionSplitIntoTokensArray)
            {
                token.Trim(); //trimming helps with eliminating whitespaces

                //skip the empty tokens. They were just whitespaces before trimming.
                if (token.Length == 0)
                {
                    continue;
                }

                //The following works for true cases of a token being an integer.
                if (int.TryParse(token, out numInteger))
                {
                    //operator stack has to have at least one operator to complete the following:
                    if (operatorStack.Count >= 1)
                    {
                        if ('*'.Equals(operatorStack.Peek()))
                        {
                            operatorStack.Pop();
                            numInteger = valueStack.Pop() * numInteger;
                        }

                        else if ('/'.Equals(operatorStack.Peek()))
                        {
                            if (numInteger == 0)
                            {
                                throw new DivideByZeroException("Undefined! Any non-zero number divided by zero is undefined!");
                            }
                            operatorStack.Pop();
                            numInteger = valueStack.Pop() / numInteger;
                        }
                        //The result integer will be added to the valueStack
                        valueStack.Push(numInteger);
                    }
                    //If operator stack is empty:
                    else
                    {
                        valueStack.Push(numInteger);
                    }
                }


                //else, if token is variable, continue here:
                //the regular expression is created using the information on the following websites:
                //https://www3.ntu.edu.sg/home/ehchua/programming/howto/Regexe.html
                //https://chortle.ccsu.edu/finiteautomata/Section07/sect07_2.html
                //https://stackoverflow.com/questions/6011586/what-does-this-regex-means-a-za-z-d
                else if (Regex.IsMatch(token, @"^\s*[a-zA-Z]+\d+\s*$"))
                {  
                    //Look up the variable's value and assign it into the numInteger variable:
                    numInteger = variableEvaluator(token.Trim());

                    if (operatorStack.Count >= 1)
                    {

                        if ('*'.Equals(operatorStack.Peek()))
                        {
                            operatorStack.Pop();
                            numInteger = valueStack.Pop() * numInteger;
                        }

                        else if ('/'.Equals(operatorStack.Peek()))
                        {
                            if (numInteger == 0)
                            {
                                throw new DivideByZeroException("Undefined! Any non-zero number divided by zero is undefined!");
                            }
                            operatorStack.Pop();
                            numInteger = valueStack.Pop() / numInteger;
                        }

                        //Final result or the first integer will be added to the valueStack
                        valueStack.Push(numInteger);
                    }

                    //If operator stack is empty:
                    else
                    {
                        throw new ArgumentException("Invalid expression!");
                    }
                }


                //if token is an operator, it'll continue here:
                else if (char.TryParse(token, out operatorChar))
                {
                    // if token is + or - :
                    if (operatorChar == '+' || operatorChar == '-')
                    {
                        //if operator stack is not empty, continue here:
                        if (operatorStack.Count >= 1)
                        {
                            if ('+'.Equals(operatorStack.Peek()) || '-'.Equals(operatorStack.Peek()))
                            {
                                if (valueStack.Count < 2)
                                {
                                    throw new ArgumentException("Invalid expression!");
                                }
                                else if ('+'.Equals(operatorStack.Peek()))
                                {
                                    operatorStack.Pop();
                                    numInteger = valueStack.Pop() + valueStack.Pop();
                                }
                                else if ('-'.Equals(operatorStack.Peek()))
                                {
                                    operatorStack.Pop();
                                    numInteger = valueStack.Pop();
                                    numInteger = valueStack.Pop() - numInteger;
                                }
                                valueStack.Push(numInteger);
                            }
                            else if ('*'.Equals(operatorStack.Peek()) || '/'.Equals(operatorStack.Peek()))
                            {
                                if (valueStack.Count < 2)
                                {
                                    throw new ArgumentException("Invalid expression!");
                                }
                                else if ('*'.Equals(operatorStack.Peek()))
                                {
                                    operatorStack.Pop();
                                    numInteger = valueStack.Pop() * valueStack.Pop();
                                }
                                else if ('/'.Equals(operatorStack.Peek()))
                                {
                                    if (valueStack.Peek() == 0)
                                    {
                                        throw new DivideByZeroException("Undefined! Any non-zero number divided by zero is undefined!");
                                    }

                                    operatorStack.Pop();
                                    numInteger = valueStack.Pop();
                                    numInteger = valueStack.Pop() / numInteger;
                                }
                                valueStack.Push(numInteger);
                            }
                        }

                        operatorStack.Push(operatorChar);
                    }
                    // else if token is * or / :
                    else if (operatorChar == '*' || operatorChar == '/')
                    {
                        operatorStack.Push(operatorChar);
                    }
                    // else if token is "(":
                    else if (operatorChar == '(')
                    {
                        operatorStack.Push(operatorChar);
                    }
                    // else if token is ")":
                    else if (operatorChar == ')')
                    {   
                        if(operatorStack.Count > 0) 
                        { 
                        //1st case: if top of the stack has + or -:
                        if ('+'.Equals(operatorStack.Peek()) || '-'.Equals(operatorStack.Peek()))
                        {
                            if (valueStack.Count < 2)
                            {
                                throw new ArgumentException("Invalid expression!");
                            }
                            else if ('+'.Equals(operatorStack.Peek()))
                            {
                                operatorStack.Pop();
                                numInteger = valueStack.Pop() + valueStack.Pop();
                            }
                            else if ('-'.Equals(operatorStack.Peek()))
                            {
                                operatorStack.Pop();
                                numInteger = valueStack.Pop();
                                numInteger = valueStack.Pop() - numInteger;
                            }
                            valueStack.Push(numInteger);

                            //2nd case: if the top of the stack has "(":
                            if (operatorStack.Count==0 || !'('.Equals(operatorStack.Peek()))
                            {
                                throw new ArgumentException("Invalid expression!");
                            }
                            else if ('('.Equals(operatorStack.Peek()))
                            {
                                operatorStack.Pop();
                            }
                            //3rd case: 
                            if (operatorStack.Count > 0)
                            {
                                if ('*'.Equals(operatorStack.Peek()) || '/'.Equals(operatorStack.Peek()))
                                {
                                    if (valueStack.Count < 2)
                                    {
                                        throw new ArgumentException("Invalid expression!");
                                    }
                                    else if ('*'.Equals(operatorStack.Peek()))
                                    {
                                        operatorStack.Pop();
                                        numInteger = valueStack.Pop() * valueStack.Pop();
                                    }
                                    else if ('/'.Equals(operatorStack.Peek()))
                                    {
                                        if (valueStack.Peek() == 0)
                                        {
                                            throw new DivideByZeroException("Undefined! Any non-zero number divided by zero is undefined!");
                                        }

                                        operatorStack.Pop();
                                        numInteger = valueStack.Pop();
                                        numInteger = valueStack.Pop() / numInteger;
                                    }
                                    valueStack.Push(numInteger);
                                }
                            }
                        }

                            //if top of the stack has "*" or "/":
                            else if ('*'.Equals(operatorStack.Peek()) || '/'.Equals(operatorStack.Peek()))
                        {
                            if (valueStack.Count < 2)
                            {
                                throw new ArgumentException("Invalid expression!");
                            }
                            else if ('*'.Equals(operatorStack.Peek()))
                            {
                                operatorStack.Pop();
                                numInteger = valueStack.Pop() * valueStack.Pop();
                            }
                            else if ('/'.Equals(operatorStack.Peek()))
                            {
                                if (valueStack.Peek() == 0)
                                {
                                    throw new DivideByZeroException("Undefined! Any non-zero number divided by zero is undefined!");
                                }

                                operatorStack.Pop();
                                numInteger = valueStack.Pop();
                                numInteger = valueStack.Pop() / numInteger;
                            }
                            valueStack.Push(numInteger);
                        }

                    }
                } //END of if token is ")" 

                }//END of "if token is an operator"


                //If the token is not operator or integer or variable, then throw exemption:
                else
                {
                    throw new ArgumentException("The expression contains invalid characters or the variable is invalid!");
                }


            }//END of foreach loop. All tokens are processed.


            //After all token has been processed:
            //Check if operator stack is empty:
            if (operatorStack.Count == 0)
            {

                if (valueStack.Count == 1)
                {
                    return valueStack.Pop();
                }
                //If value stack contains more than one value:
                else
                {
                    throw new ArgumentException("Wrong expression!");
                }
            }

            //else if operator stack is NOT empty:
            else if (operatorStack.Count > 0)
            {
                if (operatorStack.Count == 1 && valueStack.Count == 2 && ('+'.Equals(operatorStack.Peek()) || '-'.Equals(operatorStack.Peek())))
                {
                    if ('+'.Equals(operatorStack.Peek()))
                    {
                        operatorStack.Pop();
                        valueStack.Push(valueStack.Pop() + valueStack.Pop());
                    }
                    else if ('-'.Equals(operatorStack.Peek()))
                    {
                        operatorStack.Pop();
                        numInteger = valueStack.Pop();
                        valueStack.Push(valueStack.Pop() - numInteger);
                    }

                }

                //There isn't exactly one operator on the operator stack
                //or exactly two numbers on the value stack
                else
                {
                    throw new ArgumentException("Wrong expression!");
                }

            }
            return valueStack.Pop();
        }
    }
}