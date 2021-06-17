// Matthew Rutigliano
// 4 May 2021
// Revision History: Data members, constructor, retrieve, reset written 4/29/21

// Class Invariant:
// dataMirror is extension of class dataLine
// dataMirror object encapsulates integer array that is modified at instantiation
// Integer array is supplied by client at object instantiation
// Not supplying an integer array will cause object to be created in permanently deactivated state
// Supplying an integer array of length less than 1 will cause object to be created in permanently deactivated state
// Calling query with value y will return a boolean indicating if y is in the array
// Calling retrieve with value z will return an array containing the first z values of the array
//     If z is greater than the length of the array, the contents of the entire array will be returned
//     If retrieve is called with no argument, the middle two array values will be returned
//     If z is less than 1, retrieve returns a null pointer, and the object is made inactive (bad retrieve call)
//     If a certain amount of bad retrieve calls are made, the object will be permanently deactivated
//     This amount varies from object to object
// While inactive or permanently deactivated, query will always return false, and retrieve will return a null pointer
// Reset returns state to active (unless object was instantiated with no array or an array with length less than 1)
// Object cannot be reset in case of permanent deactivation (reset returns false)
// State returns boolean indicating if object is active (true) or inactive (false)
// Perm_Deactivated returns boolean indicating if object has been permanently deactivated (true)
// Client must track active state, permanently deactivated state

// Interface Invariant:
// Client must track active state, permanently deactivated state
// Inactive or permanently deactivated object will return false for query, null for retrieve
// Permanently deactivated object will return false for reset

using System;
using System.Collections.Generic;
using System.Text;

namespace P5
{
    public class dataMirror: dataLine
    {
        private static int OBJECT_COUNT = 0;
        private static int BOUND_MULTIPLIER = 2;
        private int RETRIEVE_BOUND;
        private int failed_retrieves;
        private bool isActive;

        // Postcondition: Object may become permanantly deactivated
        public dataMirror(int[] arr = null): base(arr)
        {
            RETRIEVE_BOUND = ++OBJECT_COUNT * BOUND_MULTIPLIER;
            failed_retrieves = 0;
            isActive = (arr != null);
            if (isActive)
                mirrorData();
        }

        // Postcondition: Object may become inactive, permanently deactivated
        public override int[] retrieve(int count)
        {
            int[] result = null;
            if (isActive)
            {
                result = base.retrieve(count);
                if (result == null)
                    isActive = ++failed_retrieves < RETRIEVE_BOUND;
            }
            return result;
        }

        public int[] retrieve()
        {
            int[] middle = null;
            if (isActive && base.State)
            {
                middle = new int[] { data[data.Length/2], data[data.Length/2] };
            }
            return middle;
        }

        // Postcondition: Object may become active
        public override bool reset()
        {
            if (isActive)
                return base.reset();
            return false;
        }

        private void mirrorData()
        {
            int[] copy = new int[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
                copy[i] = data[i];
            for (int i = 0; i < data.Length; i++)
                copy[copy.Length - (i + 1) ] = copy[i];
            data = copy;
        }

        public bool Perm_Deactivated
        {
            get => !isActive;
        }
    }
}

// Implementation Invariant:
// State and isActive parameters control query, retrieve
// isActive parameter controls reset
// Perm_Deactivated returns inverse of isActive; isActive=true means object hasn't been permanently deactivated
// Array is modified to add the original values in reverse order
// Parent's array is modified directly at instantiation with mirrorX
// Retrieve is overriden to track failed retrieves, permanently deactivate object if they exceed RETRIEVE_BOUND
// RETRIEVE_BOUND calculated by multiplying object count with BOUND_MULTIPLIER
// Reset is overriden to check if object has been permanently deactivated