// Matthew Rutigliano
// 6 June 2021
// Revision History: Functions written 6/3/21

// Program Description: A collection of dataGuards is created, with each dataGuard containing a different combination
// of dataLine and guard
// All the dataLine subobjects are tested as in P3
// All the guard objects have value called enough times to change their modes

using System;

namespace P5
{
    class P5
    {
        static void Main(string[] args)
        {
            int numSets = 1;
            dataGuard[] dataGuardArr = initDataGuardArr(numSets);

            retrieveAll(dataGuardArr, 100, true);

            queryAll(dataGuardArr, 1);

            for (int i = 0; i < 3; i++)
            {
                retrieveAll(dataGuardArr, -5, false);
                resetAll(dataGuardArr);
            }

            queryAll(dataGuardArr, 7);

            retrieveAll(dataGuardArr, 4, true);

            valueAll(dataGuardArr, 100, true);

            for (int i = 0; i < 4; i++)
                valueAll(dataGuardArr, 100, false);

            valueAll(dataGuardArr, 100, true);
            valueAll(dataGuardArr, 2, true);


        }

        static dataGuard[] initDataGuardArr(int numSets)
        {
            dataGuard[] arr = new dataGuard[numSets * 9];
            string[] dlTypes = { "dataLine", "dataMirror", "dataInsert" };
            string[] gTypes = { "guard", "skipGuard", "quirkyGuard" };
            int[] dlInputArr = { 1, 2, 3, 4, 5 };
            char[] guardInputArr = { 'a', 'b', 'c', 'd', '?' };
            int index = 0;
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    arr[index++] = new dataGuard(dlTypes[i], dlInputArr, gTypes[j], guardInputArr);
                }
            }
            return arr;
        }

        static void retrieveAll(dataGuard[] arr, int val, bool printResults)
        {
            int[] results = null;
            Console.WriteLine($"Retrieving contents of each object with y={val}:");
            for (int i = 0; i < arr.Length; i++)
            {
                results = arr[i].retrieve(val);
                if (printResults)
                {
                    Console.Write($"{i}: {arr[i].DataLineType} ");
                    if (results != null)
                    {
                        foreach (int x in results)
                            Console.Write($"{x}, ");
                    }
                    else
                        Console.Write("Null result");
                    Console.WriteLine();
                }
            }
        }

        static void queryAll(dataGuard[] arr, int val)
        {
            bool result = false;
            Console.WriteLine($"Querying each object with y={val}");
            for (int i = 0; i < arr.Length; i++)
            {
                result = arr[i].query(val);
                Console.WriteLine($"{i}: {result}");
            }
        }

        static void resetAll(dataGuard[] arr)
        {
            Console.WriteLine($"Resetting all objects in inactive state");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{i} state: {arr[i].getDataLineState()}    ");
                if (!arr[i].getDataLineState())
                    Console.Write($"State after reset: {arr[i].dataLineReset()}");
                Console.WriteLine();
            }
        }

        static void valueAll(dataGuard[] arr, int val, bool printResults)
        {
            char[] result;
            Console.WriteLine($"Running value on each object with y={val}");
            for (int i = 0; i < arr.Length; i++)
            {
                result = arr[i].value(val);
                if (printResults)
                {
                    Console.Write($"{i}: Type:{arr[i].GuardType}  ");
                    foreach (char x in result)
                        Console.Write($"{x}, ");
                    Console.WriteLine();
                }
            }
        }
    }
}
