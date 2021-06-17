// Matthew Rutigliano
// 4 May 2021
// Revision History: Data members, constructor, query written 5/1/21

// Class Invariant:
// dataInsert extension of dataLine class
// dataInsert object encapsulates integer array that is modified at instatiation
// Integer array is supplied by client at object instantiation
// Not supplying an integer array will cause object to be created in permanent inactive state
// Supplying an integer array of length less than 1 will cause object to be created in permanent inactive state
// Calling query with value y will return a boolean indicating if y is in the array
// Calling retrieve with value z will return an array containing the first z values of the array
//     If z is greater than the length of the array, the contents of the  entire array will be returned
//     If z is less than 1, retrieve returns a null pointer, and the object is made inactive
// While inactive, query will always return false, and retrieve will return a null pointer
// Reset returns state to active (unless object was instantiated with no array or an array with length less than 1)
// State returns boolean indicating if object is active (true) or inactive (false)
// Client must track active state

// Interface Invariant:
// Client must track active state
// Inactive object will return false for query, null for retrieve

using System;
using System.Collections.Generic;
using System.Text;

namespace P5
{
    public class dataInsert: dataLine
    {
        private static int OBJECT_COUNT = 0;
        private static int BOUND_MULTIPLIER = 2;
        private int A_INDEX;
        private int QUERY_BOUND;
        private int queryCount;

        // Postcondition: Object may become inactive
        public dataInsert(int[] arr = null) : base(arr)
        {
            QUERY_BOUND = ++OBJECT_COUNT * BOUND_MULTIPLIER;
            queryCount = 0;
            if (arr != null)
                insertData();
        }

        public override bool query(int y)
        {
            bool result = base.query(y);
            if (++queryCount >= QUERY_BOUND)
                data[A_INDEX] = QUERY_BOUND;
            data[maxIndex()]--;
            return result;
        }

        private void insertData()
        {
            A_INDEX = data.Length;
            int[] insert = new int[(2 * A_INDEX) + 1];
            int a = 0;
            for(int i=0; i< A_INDEX; i++)
            {
                a += data[i];
                insert[i] = data[i];
                insert[i + A_INDEX + 1] = data[i];
            }
            insert[A_INDEX] = a * (OBJECT_COUNT + 1);
            data = insert;
        }

        private int maxIndex()
        {
            int max = 0;
            for (int i = 1; i < data.Length; i++)
                max = (data[max] > data[i]) ? max: i;
            return max;
        }
    }
}

// Implementation Invariant:
// State parameter controls query, retrieve
// Value X is added to the end of parent's array, then a copy of the original array is appended to that
// X is added by insertX at instantiation
// X is calculated by multiplying the sum of all the array values with object count plus 1
// After query has been called QUERY_BOUND times, X is replaced with QUERY_BOUND
// QUERY_BOUND is calculated by multiplying object count with BOUND_MULTIPLIER
// Each query decrements the max value in the array
// Query is overriden to enable this behavior