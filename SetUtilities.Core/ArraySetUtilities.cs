using System;
using System.Collections.Generic;

namespace SetUtilities.Core
{
    /// <summary>
    /// A set of Utiltities that will Set-based operations on List<int> datasets
    /// </summary>
    public sealed class ArraySetUtilities
    {
        /// <summary>
        /// Will create a List of integers within an inclussive range.  Option to generate unique elements is inlcuded
        /// </summary>
        /// <param name="size">Number of elements (must be > 1)</param>
        /// <param name="minimum">Minimum value for the integers to generate</param>
        /// <param name="maximum">Maximum value for the integers to generate</param>
        /// <param name="uniqueElements">Whether elements must be unique</param>
        /// <returns></returns>
        public static List<int> CreateSet(int size, int minimum, int maximum, bool uniqueElements)
        {
            bool filled = false;

            List<int> arraySet = null;  
            Random random = new Random(); 
            if (size <= 0 || (uniqueElements && (maximum - minimum) <size))
                throw new ArgumentException("invalid size specified");

            if (uniqueElements)
            {
                int rSize = maximum - minimum + 1;
                int[] rawNumbers = new int[rSize + 1];
                for (int i = 0; i < rSize; i++)
                    rawNumbers[i] = minimum + i;
                arraySet = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    int randIndex = random.Next(0, rSize - 1 - i);
                    arraySet.Add(rawNumbers[randIndex]);
                    int temp = rawNumbers[rSize -i];
                    rawNumbers[rSize - i] = rawNumbers[randIndex];
                    rawNumbers[randIndex] = temp;
                }
            }
            else
            {
                // Non-Unique case 
                int i = 0;
                arraySet = new List<int>();
                while (!filled)
                {
                    int randi = random.Next(minimum, maximum);
                    arraySet.Add(randi);
                    i++;
                    filled = (i == size);
                }
            }
            return arraySet;
        }

        /// <summary>
        /// Boolean
        /// Method to determine if a List contains uniques elements
        /// </summary>
        /// <param name="arraySet">The data set to test</param>
        /// <returns>treu if the elements are unique, false otherwise</returns>
        public static bool IsUnique(List<int> arraySet)
        {
            bool isUniqueflag = true; // always runs

            for (int i = 0; i < arraySet.Count && isUniqueflag; i++) 
            {
                for (int j = i + 1; j < arraySet.Count && isUniqueflag; j++)
                {
                    isUniqueflag = arraySet[i] != arraySet[j];
                }
            }
            return isUniqueflag;
        }


        /// <summary>
        ///  Determines the common elements(intersection) between setA and setB.
        /// </summary>
        /// <param name="setA">The first of the two Sets</param>
        /// <param name="setB">The second of the two sets</param>
        /// <returns>a new set that consists of the common elements that exist in A and B</returns>
        public static List<int> Intersection(List<int> setA, List<int> setB)
        {
            List<int> setCommon = new List<int>();  
            if (setA.Count == 0 || setB.Count == 0)
            {
                throw new ArgumentException("Arraylist arguments cannot be empty");
            }
            for (int i = 0; i < setA.Count; i++)   
            {
                for (int j = 0; j < setB.Count; j++)
                {
                    if (setA[i] == setB[j])        
                    {
                        bool canAddToCommon = true; 
                        for (int k = 0; k < setCommon.Count; k++) 
                        {
                            if (setA[i] == setCommon[k])
                            {
                                canAddToCommon = false;
                                break;
                            }
                        }
                        if (canAddToCommon) 
                        {
                            setCommon.Add(setA[i]);
                        }
                    }
                }
            }

            return setCommon;
        }

        /// <summary>
        /// Determine the union of two sets
        /// </summary>
        /// <param name="setA">the first of the two sets for the union</param>
        /// <param name="setB">the second of the two sets for the union</param>
        /// <returns>a union between setA and setB</returns>
        public static List<int> union(List<int> setA, List<int> setB)
        {
            List<int> uniqueSet = new List<int>();
            if (setA.Count == 0 || setB.Count == 0)
            {
                throw new ArgumentException("Arraylist arguments cannot be empty");
            }

            for (int i = 0; i < setA.Count; i++)//setA first -> end.
            {
                bool canAddToUnique = true;
                for (int j = 0; j < uniqueSet.Count && canAddToUnique; j++)//setB second => end.
                {
                    canAddToUnique = setA[i] != uniqueSet[j];
                }
                if (canAddToUnique)
                {
                    uniqueSet.Add(setA[i]);
                }
            }

            for (int i = 0; i < setB.Count; i++)
            {
                bool canAddToUnique = true;
                for (int j = 0; j < uniqueSet.Count && canAddToUnique; j++)
                {
                    canAddToUnique = setB[i] != uniqueSet[j];
                }
                if (canAddToUnique)
                {
                    uniqueSet.Add(setB[i]);
                }
            }

            return uniqueSet;
        }


        /// <summary>
        /// Determines if setB is a subSet of setA
        /// </summary>
        /// <param name="setA">the full set for the comparison</param>
        /// <param name="setB">the sub set to be tested</param>
        /// <returns>true if setB is a subSet of setA</returns>
        public static bool subSet(List<int> setA, List<int> setB)
        {
            if (setA.Count == 0 || setB.Count == 0)
            {
                throw new ArgumentException("Arraylist arguments cannot be empty");
            }

            bool isInSet = true;
            for (int i = 0; i < setA.Count && isInSet; i++)
            {
                isInSet = false;
                for (int j = 0; j < setB.Count && !isInSet; j++)
                {
                    isInSet = setA[i] == setB[j];
                }
            }
            return isInSet;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Must use Test facility (Test/Run/All Tests) to run the units Tests");
            Console.WriteLine("Hit any key to close...");
            Console.ReadLine();
            //checks every value in setA, so long as:
                //isInSet is true
                //so it runs on the first passtrhough it returns true.
                //so the later half can't run, and it moves on to the next value in set A to check because the value has been found in setB.
                //for the second value in setA, the first value j in setB will set isInSet to false so it can continue.
                //because the condition is false, its alliowed to run again.
        }
    }
}
