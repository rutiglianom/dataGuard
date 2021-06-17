// Matthew Rutigliano
// 6 June 2021
// Revision History: Data members, constructor, value written 6/2/21

// Class Invariant: skipGuard object is extension of guard class
// skipGuard encapsulates character array
// Character array must be supplied by client at object instantiation
// Character array must contain a punctuation mark or object will be permanently inactive (state=false)
// Calling value with argument x returns the first x characters in the internal array
// When mode=true, the characters will be uppercase
// When mode=false, the characters will be lowercase
// Mode toggles after a certain amount of value calls
// Calling value with x less than 1 will return a null pointer


// Interface Invariant:
// Client must make sure state is true after creating object
// When state is false, value will always return null

using System;
using System.Collections.Generic;
using System.Text;

namespace P5
{
    public class skipGuard: guard
    {
        private readonly int skip;
        private const int skipDivisor = 2;

        public skipGuard(char[] arr = null): base(arr)
        {
            if (data != null)
                skip = data.Length / skipDivisor;
        }

        public override char[] value(int count = -1)
        {
            bool lastMode = base.Mode;
            char[] result = base.value(count);
            char[] skipResult = result;
            if (result != null && count % skip == 0)
            {
                skipResult = new char[result.Length - 1];
                for (int i = 0; i < skip-1; i++)
                    skipResult[i] = result[i];
                for (int i = skip - 1; i < skipResult.Length; i++)
                    skipResult[i] = lastMode ? char.ToUpper(result[i + 1]) : char.ToLower(result[i + 1]);
            }
            return skipResult;
        }
    }
}

// Implementation Invariant:
// Amount of value calls needed to change mode is the product of the length of the internal array and a scaler
// State parameter controls value
// Value result doesn't contain array value with index=skip if the argument of value is divisible by skip
// Skip is determined by dividing the length of the internal array by a scaler
