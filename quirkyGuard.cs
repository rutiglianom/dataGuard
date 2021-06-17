// Matthew Rutigliano
// 6 June 2021
// Revision History: Data members, constructor, value written 6/2/21

// Class Invariant: quirkyGuard object is extension of guard class
// guardGuard encapsulates character array
// Character array must be supplied by client at object instantiation
// Character array must contain a punctuation mark or object will be permanently inactive (state=false)
// Calling value with argument x returns the first x characters in the internal array
// When mode=true, the characters will be uppercase
// When mode=false, the characters will be lowercase
// Mode toggles after a certain amount of value calls
// Mode will only toggle a limited amount of times before permanently settling on a last mode
// Calling value with x less than 1 will return a null pointer


// Interface Invariant:
// Client must make sure state is true after creating object
// When state is false, value will always return null

using System;
using System.Collections.Generic;
using System.Text;

namespace P5
{
    public class quirkyGuard: guard
    {
        private readonly int modeLimit;
        private int modeCount;

        public quirkyGuard(char[] arr = null): base(arr)
        {
            if (data != null)
                modeLimit = data.Length;
            modeCount = 0;
        }

        public override char[] value(int count = -1)
        {
            state = count > MIN_LENGTH;
            char[] subset = null;

            if (state)
            {
                int size = ((count < data.Length) ? count : data.Length) + 1;
                subset = new char[size];
                bool toggle = mode;
                for (int i = 0; i < size-1; i++)
                {
                    subset[i] = toggle ? char.ToUpper(data[i]) : char.ToLower(data[i]);
                    toggle = !toggle;
                }
                subset[size - 1] = mode ? '+' : '/';
            }
            if (modeCount < modeLimit)
            {
                if (++valueCount % valueLimit == 0)
                {
                    mode = !mode;
                    modeCount++;
                }
            }
            return subset;
        }
    }
}

// Implementation Invariant:
// Amount of value calls needed to change mode is the product of the length of the internal array and a scaler
// State parameter controls value
// Amount of permitted toggles is determined by the length of the internal array
// Value results have '+' appended to end when mode=true, '/' when mode=false
// Results will always be in alternating uppercase and lowercase, but case of first entry will vary with mode