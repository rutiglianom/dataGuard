// Matthew Rutigliano
// 6 June 2021
// Revision History: Data members, constructor, dataLine functions, guard functions written 6/2/21

// Class Invariant: dataGuard class is combination of dataLine and guard
// When initializing dataGuard, client supplies int array for dataLine, string specifying type of dataLine,
// char array for guard, string specifying type of guard
// Capitalization of strings doesn't matter
// String formatting:     "dataline" -> dataline
//                      "datamirror" -> dataMirror
//                      "datainsert" -> dataInsert
//                           "guard" -> guard
//                       "skipguard" -> skipGuard
//                     "quirkyguard" -> quirkyGuard
// dataGuard can be created with only one of the two types by leaving the appropriate fields blank
// This allows a dataGuard with a dataLine but no guard, or a guard but no dataLine
// dataGuard supports all key functions of each type (query, retrieve, reset, value)
// dataLineType and guardType return string specifying type, formatted as above
// dataPresent and guardPresent specify if dataLine and guard have been initialized


// Interface Invariant:
// dataPresent and guardPresent must be monitored by client
// query, dataLineReset and getDataLineState will return false, retrieve will return null if dataPresent is false
// getGuardState and getGuardMode will return false, value will return null if guardPresent is false
// States of dataLine and guard must be monitored as specified in those classes

using System;
using System.Collections.Generic;
using System.Text;

namespace P5
{
    public class dataGuard
    {
        private dataLine dataLineParent;
        private guard guardParent;
        private string dataLineType;
        private string guardType;
        bool dataPresent;
        bool guardPresent;

        public dataGuard(string dlType = "", int[] dataArr = null, string gType = "", char[] guardArr = null)
        {
            dataPresent = guardPresent = true;
            dataLineType = dlType.ToLower();
            guardType = gType.ToLower();
            initDataLine(dataLineType, dataArr);
            initGuard(guardType, guardArr);
        }

        private void initDataLine(string dataLineType, int[] dataArr)
        {
            switch (dataLineType)
            {
                case "dataline":
                    dataLineParent = new dataLine(dataArr);
                    break;
                case "datainsert":
                    dataLineParent = new dataInsert(dataArr);
                    break;
                case "datamirror":
                    dataLineParent = new dataMirror(dataArr);
                    break;
                default:
                    dataPresent = false;
                    break;
            }
        }

        private void initGuard(string guardType, char[] guardArr)
        {
            switch (guardType)
            {
                case "guard":
                    guardParent = new guard(guardArr);
                    break;
                case "skipguard":
                    guardParent = new skipGuard(guardArr);
                    break;
                case "quirkyguard":
                    guardParent = new quirkyGuard(guardArr);
                    break;
                default:
                    guardPresent = false;
                    break;
            }
        }

        public bool DataLinePresent
        {
            get => DataLinePresent;
        }

        public bool GuardPresent
        {
            get => guardPresent;
        }

        // dataLine Functions
        public bool query(int y)
        {
            bool result = false;
            if (dataPresent)
                result = dataLineParent.query(y);
            return result;
        }

        public int[] retrieve(int count = -1)
        {
            int[] result = null;
            if (dataPresent)
                result = dataLineParent.retrieve(count);
            return result;
        }

        public bool dataLineReset()
        {
            bool result = false;
            if (dataPresent)
                result = dataLineParent.reset();
            return result;
        }

        public bool getDataLineState()
        {
            bool result = false;
            if (dataPresent)
                result = dataLineParent.State;
            return result;
        }


        // Guard Functions
        public char[] value(int count = -1)
        {
            char[] result = null;
            if (guardPresent)
                result = guardParent.value(count);
            return result;
        }

        public bool getGuardState()
        {
            bool result = false;
            if (guardPresent)
                result = guardParent.State;
            return result;
        }

        public bool getGuardMode()
        {
            bool result = false;
            if (guardPresent)
                result = guardParent.Mode;
            return result;
        }

        public string DataLineType
        {
            get => dataLineType;
        }

        public string GuardType
        {
            get => guardType;
        }
    }
}

// Implementation Invariant:
// dataPresent controls query, retrieve, resetDataLine, get dataLineState
// guardPresent controls value, getGuardState, getGuardMode