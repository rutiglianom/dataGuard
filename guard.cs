// Matthew Rutigliano
// 6 June 2021
// Revision History: Data members, constructor, value written 6/1/21

// Class Invariant: Guard object encapsulates character array
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
    public class guard
    {
        protected bool state;
        protected bool mode;
        protected int valueCount;
        protected int valueLimit;
        protected char[] data;
        protected const int MIN_LENGTH = 0;
        private const int VALUE_LIMIT_SCALER = 1;

        // Postcondition: State may change to false
        public guard(char[] arr = null)
        {
            mode = true;
            state = false;
            data = new char[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                data[i] = arr[i];
                if (!state)
                {
                    if (data[i] == '.' || data[i] == ',' || data[i] == '!' || data[i] == '?' || data[i] == '"'
                      || data[i] == '(' || data[i] == ')' || data[i] == '/' || data[i] == ';' || data[i] == ':' || data[i] == '\'')
                        state = true;
                }
            }
            valueCount = 0;
            valueLimit = data.Length * VALUE_LIMIT_SCALER;
        }

        // Postcondition: Mode may toggle
        public virtual char[] value(int count = -1)
        {
            char[] subset = null;

            if (state && count > MIN_LENGTH)
            {
                int size = (count < data.Length) ? count : data.Length;
                subset = new char[size];
                for (int i = 0; i < size; i++)
                    subset[i] = mode ? char.ToUpper(data[i]) : char.ToLower(data[i]);
            }
            mode = (++valueCount % valueLimit == 0) ? !mode : mode;
            return subset;
        }

        public bool State
        {
            get => state;
        }

        public bool Mode
        {
            get => mode;
        }
    }
}

// Implementation Invariant:
// Amount of value calls needed to change mode is the product of the length of the internal array and a scaler
// State parameter controls value
// Value is virtual to enable class extensions
