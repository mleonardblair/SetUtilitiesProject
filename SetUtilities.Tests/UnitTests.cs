/*Bug Summary
  +++++++++++      
   #1   Description: Subset doesn't return true when all the values in setB are within setA 
                    despite the method return XML comments indicating that it should.
        TestMethods: TestSubset_SetALargerThanSubsetB(),
                     TestSubset_SetsEquivalent(), 
                     TestSubset_SubsetBiggerThanSet()
        Suggested Fix: Line 182, 185 , isInSet to false means if there is a trailing value in setA not in setB will return false- 
                    this indicates that there needs to be a replace of setA.count on Line 182,with setB.count on Line 185, and setB.count on line 185
                    should be should suffice.
    
   #2   Description: Subset doesn't return false when (subset) setB is out of order from main setA's order.
        TestMethods: TestSubset_SubsetNotInOrderLastElementEven(), 
                     TestSubset_SubsetNotInOrderLastElementOdd(),
        SuggestedFix: Line I'm assuming there was a mistakenly placed ! in front of the ! isInSet, as well as potentially
                        the false being in place of a true. If both of these are done then the method will check for a false
                        and if it finds one, as a true subset, it will stop checking as even one means its own of order and not a subset.

  #3    Description: CreateSet should not be sometimes having values that are less than the minimum. By making an easy change, no less than minimum 
                     values are returned.
        TestMethods: TestCreateSet_NoValuesLessThanMinimum()
        SuggestedFix: Line 43  - int temp = rawNumbers[rSize -i]; should be int temp = rawNumbers[rSize -1-i];
                        there was a -1 missing in the index. By adding this, it eliminates any opportunity for a 0 to occur in arraySet.
   
  #4    Description: CreateSet should error when it has a set size of 1 according to the XML comments, 
                     it should require a set size of 2. When entering a set size of 1, it doesn't error.
        TestMethods: TestCreateSet_SetSizeDesiredLessThanTwo()
        SuggestedFix: Line 18,29  
 */
using SetUtilities.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SetUtilities.Tests
{
    [TestClass]
    public class UnitTests
    {
        /// <summary>
        /// The expectation is that all values should be unique that are created within the right range.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_NoNonDistinctNegativeValuesLessThanMinimum()
        {
            int size = 5;
            int minimum = -15;
            int maximum = -11;
            bool uniqueElements = false;

            List<int> actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);

            for (int i = 0; i < actualResult.Count; i++)
            {
                if (actualResult[i] < minimum)
                {
                    Assert.Fail("The values are less than minimum.");
                }
            }
        }

        /// <summary>
        /// The expectation is that all values should be unique that are created within the right range.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_NoNonDistinctNegativeValuesGreaterThanMaximum()
        {
            int size = 6;
            int minimum = -14;
            int maximum = -10;
            bool uniqueElements = false;

            List<int> actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);

            for (int i = 0; i < actualResult.Count; i++)
            {
                if (actualResult[i] > maximum)
                {
                    Assert.Fail("The values are greater than maximum.");
                }
            }
        }

        /// <summary>
        /// This should not return true. The method should not allow for a size of less than 2 to be able to run according to XML comments.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_SetSizeDesiredLessThanTwo()
        {
            int size = 1;
            int minimum = 1;
            int maximum = 15;
            bool uniqueElements = true;

            List<int> actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);

            CollectionAssert.AllItemsAreUnique(actualResult);
        }
        /// <summary>
        /// Checks if negative values are treated as positive, and vice verse when checked if unique.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestUnion_IsUniqueNegative()
        {
            //Act
            var SetA = new List<int>() { 111, 100, -100 };
            var SetB = new List<int>() { 111, 100, -100 };

            var expectedResult = new List<int>() { 111, 100, -100 };

            var actualResult = ArraySetUtilities.union(SetA, SetB);

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// Checks if negative values are treated as positive, and vice verse when checked if unique.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestUnion_IsUniqueNegativeDifferentSizeLists()
        {
            //Act
            var SetA = new List<int>() { 111, 100, -100, -115 };
            var SetB = new List<int>() { 111, 100, -100, -115, 115};

            var expectedResult = new List<int>() { 111,100,-100,-115,115 };

            var actualResult = ArraySetUtilities.union(SetA, SetB);

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }


        /// <summary>
        /// The expectation on method call is that a method with different lengths will result in setA + setB minus any present in setA.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestUnion_CheckDuplicates_SetBLargerThanA_DifferentLengths()
        {
            //Act
            var SetA = new List<int>() { 31, 34, 37, 37 };
            var SetB = new List<int>() { 31, 33, 34, 94, 37, 37 };
            var expectedResult = new List<int>() { 31, 34, 37, 33, 94 };

            var actualResult = ArraySetUtilities.union(SetA, SetB);

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// The expectation on method call is that a method with different lengths will result in setA + setB minus any present in setA.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestUnion_CheckDuplicates_SetALargerThanB_DifferentLengths()
        {
            //Act
            var SetA = new List<int>() { 331, 333, 334, 934, 337, 337 };
            var SetB = new List<int>() { 331, 333, 334, 934};
            var expectedResult = new List<int>() { 331, 333, 334, 934, 337 };

            var actualResult = ArraySetUtilities.union(SetA, SetB);

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// The expectation on method call is that a method with different lengths will result in setA + setB minus any present in setA.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestUnion_CheckDifferentLengths()
        {
            //Act
            var SetA = new List<int>() { 11, 14, 17};
            var SetB = new List<int>() { 11, 13, 14, 94, 17 };
            var expectedResult = new List<int>() { 11, 14, 17, 13, 94 };

            var actualResult = ArraySetUtilities.union(SetA, SetB);

            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// This should return the list of values minus any collection duplicates in either sets.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestUnion_CheckWithOutDuplicates()
        {
            //Act
            var SetA = new List<int>() { 1, 4, 7, 10, 13 };
            var SetB = new List<int>() { 2, 5, 8, 11, 14 };
            var expectedResult = new List<int>() { 1, 4, 7, 10, 13, 2, 5, 8, 11, 14 };

            var actualResult = ArraySetUtilities.union(SetA, SetB);

            CollectionAssert.AreEqual(actualResult, expectedResult);

        }

        /// <summary>
        /// This should return the list of values minus any collection duplicates in either sets.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestUnion_CheckWithDuplicates()
        {
            //Act
            var SetA = new List<int>() { 1, 4, 7, 10, 13 };
            var SetB = new List<int>() { 2, 5, 8, 10, 13 };
            var expectedResult = new List<int>() { 1, 4, 7, 10, 13, 2, 5, 8 };

            var actualResult = ArraySetUtilities.union(SetA, SetB);

            CollectionAssert.AreEqual(actualResult, expectedResult);

        }

        /// <summary>
        /// This will pass a setA with zero elements, and will thus expect an exception.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUnion_SetAEmpty()
        {
            //Arrange 
            List<int> SetA = new List<int>();
            List<int> SetB = new List<int>() { 1, 2, 3, 4, 5 };
            
            //Act
            var actualResult = ArraySetUtilities.union(SetA, SetB);
        }

        /// <summary>
        /// This will pass a setB with zero elements, and will thus expect an exception.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUnion_SetBEmpty()
        {
            //Arrange 
            List<int> SetA = new List<int>() { 10, 20, 30, 40, 50 };
            List<int> SetB = new List<int>();
            //Act
            var actualResult = ArraySetUtilities.union(SetA, SetB);
        }

        /// <summary>
        /// When called, this method will Check if all the values are distinct. The expectation is that all are unique.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_CheckAllUnique()
        {

            int size = 49;
            int minimum = 1;
            int maximum = 50;
            bool uniqueElements = true;

            var actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);
            CollectionAssert.AllItemsAreUnique(actualResult);

        }
        /// <summary>
        /// When called this method tests when unique elements is set to false, that all values in a created set
        /// are not less than minimum value in range.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_NonDistinctValuesAnyLessThanMin()
        {
            int size = 49;
            int minimum = 1;
            int maximum = 50;
            bool uniqueElements = false;


            var actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);
            bool lessThanMin = false;
            for (int i = 0; i < actualResult.Count; i++)
            {
                if (actualResult[i] < minimum)
                {
                    Assert.Fail("one item is less than the minimum");
                }
            }
        }

        /// <summary>
        /// When called this method tests when unique elements is set to false, that all values in a created set
        /// are not greater than maximum value in range.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_NonDistinctValuesAnyGreaterThanMax()
        {
            int size = 2;
            int minimum = 50;
            int maximum = 100;
            bool uniqueElements = false;


            var actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);
            bool lessThanMin = false;
            for (int i = 0; i < actualResult.Count; i++)
            {
                if (actualResult[i] > maximum)
                {
                    Assert.Fail("one item is greater than the maximum");
                }
            }
        }
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_NegativeValues()
        {
            //Arrange 
            //int size, int minimum, int maximum, bool uniqueElements
            int size = 9; // 0-8 inclusive.
            int minimum = -10;
            int maximum = -1;
            bool uniqueElements = true;
            var expectedResult = new List<int>(){ -1,-2,-3,-4,-5,-6,-7,-8,-9};
            var actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);

        }
        /// <summary>
        /// On call, this method should cause an error and that is our expectation.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSet_MinGreaterThanMax()
        {
            //Arrange 
            int size = 2; 
            int minimum = 3;
            int maximum = 1;
            bool uniqueElements = true;
            var actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);
            
        }
        /// <summary>
        /// When called, this test to pass should throw an error because the CreateSet method is malformed, and contain's as a possible value
        /// of returned list being 0, lower than the minimum value.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestCreateSet_NoValuesLessThanMinimum()
        {
            //Arrange 
            //int size, int minimum, int maximum, bool uniqueElements
            int size = 9; // 0-8 inclusive.
            int minimum = 1;
            int maximum = 10; 
            bool uniqueElements = true; 

            var actualResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);

            for (int i = 0; i < actualResult.Count; i++)
            {
                if (actualResult[i] < minimum)
                {
                    Assert.Fail("one item is less than the minimum");
                }
            }

        }

        /// <summary>
        /// When called, this test to fail will pass an invalid desired list size parameter, and should throw an error.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSet_BadSizeOfSet()
        {
            //Arrange 
            int size = 0;
            int minimum = 1;
            int maximum = 2;
            bool uniqueElements = false; // if true, will make sure that results are unique, otherwise they could still be unique by chance, we just dont care.

            List<int> actualResult = ArraySetUtilities.CreateSet
                (size, minimum, maximum, uniqueElements);
        }
        /// <summary>
        /// The expectation is the exception will be hit, due to a smaller range than number of distinct ('unique' = true) numerical integrals possible, that can be generated.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSet_InCorrectSizeCreated()
        {
            //Arrange 
            int size = 10;
            int minimum = 5;
            int maximum = 10;
            bool uniqueElements =true;
            List<int> expectedResult = ArraySetUtilities.CreateSet(size, minimum, maximum, uniqueElements);
            //Act
            List<int> actualResult = ArraySetUtilities.CreateSet
                (size, minimum, maximum, uniqueElements);

        }
        /// <summary>
        /// This when called should be expected to pass. That is because it is testing IsUnique() method
        /// that every value in passed list is itself distinct from each other.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIsUnique_Unique_NoValuesPassed() 
        { 
            // AAA :        Arrange         Act         Assert 
            //Arramge - added input number or sequence we are going to test. aswell as the result we are expecting. Both are arrange.
            List<int> arraySet = new List<int>(); //dataset/input sequence.
            bool expResult = true;                                      // expected result
            //Act - every test case should have an action, the thing we are testing. (if its unique).
            bool result = ArraySetUtilities.IsUnique(arraySet); //perform action -> actual result
            //Assert
            Assert.AreEqual(expResult, result, 
                "Unique Array test returning true -Testarray is unique");
        }
        /// <summary>
        /// When called, this method will test that the list passed is greater than 0.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIsUnique_NotUnique()
        {
            // AAA :        Arrange         Act         Assert 
            //Arrange - added input number or sequence we are going to test. aswell as the result we are expecting. Both are arrange.
            List<int> arraySet = new List<int>() { -1, 2, -1, 15 }; 
            bool expResult = false;                                  
            //Act - every test case should have an action, the thing we are testing. (if its unique).
            bool result = ArraySetUtilities.IsUnique(arraySet); 
            //Assert
            Assert.AreEqual(expResult, result,
                "Unique Array test returning false-Testarray is not unique");
        }

        /// <summary>
        /// When called, this method will test if negative values are counted towards itself without the negative sign.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIsUnique_NegativeValues()
        {
            List<int> arraySet = new List<int>() { 10, -10 };
            bool expResult = true;                                      // expected result
            //Act - every test case should have an action, the thing we are testing. (if its unique).
            bool result = ArraySetUtilities.IsUnique(arraySet); //perform action -> actual result
            //Assert
            Assert.AreEqual(expResult, result,
                "Unique Array test returning false-Testarray is not unique");
        }



        /// <summary>
        /// When called, this method will test that the list passed is greater than 0.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIsUnique_OneValuePassed()
        {
            // AAA :        Arrange         Act         Assert 
            //Arramge - added input number or sequence we are going to test. aswell as the result we are expecting. Both are arrange.
            List<int> arraySet = new List<int>() { 1 }; 
            bool expResult = true;                                      // expected result
            //Act - every test case should have an action, the thing we are testing. (if its unique).
            bool result = ArraySetUtilities.IsUnique(arraySet); //perform action -> actual result
            //Assert
            Assert.AreEqual(expResult, result,
                "Unique Array test returning false-Testarray is not unique");
        }


        /// <summary>
        /// When called, my expectation was that this when setting a size of 0 for the 'setA' there will be an 
        /// exception triggered from the Intersection method thrown. This should pass.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))] //means that this method is not expected to complete we are expected an exception.
        public void TestIntersection_SetASizeZero()
        {
            // Arrange
            List<int> setA = new List<int>();
            List<int> setB = new List<int>() { 1, 45, }; // be anything. dont re-use data sets **lose marks**
            // Act
            List<int> result = ArraySetUtilities.Intersection(setA, setB);
            // Assert - None needed for exception as no actual result is returned, and so there is nothing to compare.
        }

        /// <summary>
        /// When called, my expectation was that this when setting a size of 0 for the 'setB' there will be an 
        /// exception triggered from the Intersection method thrown. This should pass.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIntersection_SetSizeIsZero2()
        {
            // Arrange
            List<int> setA = new List<int>() { 5, 50, 4, 3 };
            List<int> setB = new List<int>();
            // Act
            List<int> result = ArraySetUtilities.Intersection(setA, setB);
            // Assert - None needed for exception as no actual result is returned, and so there is nothing to compare.
        }

        /// <summary>
        /// When called, the expectation is that the expected result of 'setA' and 'setB' distinct list values passed into intersection()
        /// will return no similiar terms.
        /// Comparing our expected result of zero intersecting terms, to the actual result, should be identical, and thus result in true/pass for
        /// this unit test. 
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIntersection_NoIntersection()
        {
            //Arrange 
            var setA = new List<int>() { 5, 2, 4, 1, 3 };
            var setB = new List<int>() { 9, 6, 10, 7, 8 };
            var expectedResult = new List<int>(); //zero like values.

            //Act
            var actualResult = ArraySetUtilities.Intersection(setA, setB);
            //Assert
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// When called, the expectation is that by passing in non unique values (repeats) of the same numerical value in setB, it will not add
        /// that value twice to the setCommon list, as it already added itself on that passthrough just before.
        /// Comparing our expected result, I expect this to pass, if it doesn't check the flag canAddToCommon.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIntersection_PartialIntersection_OneValueInSetA() // not check
        {
            //Arrange 
            var setA = new List<int>() { 90 };
            var setB = new List<int>() { 9, 90, 90, 1888 };
            var expectedResult = new List<int>() { 90 };
            //Act
            var actualResult = ArraySetUtilities.Intersection(setA, setB);
            //Assert
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// When called, the expectation is that the expected result of 'setA' and 'setB' distinct list values passed into intersection()
        /// will return two similiar collection elements.
        /// Comparing our expected result of two intersecting terms, to the actual result, should be identical, and thus result in true/pass for
        /// this unit test. 
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIntersection_PartialIntersection_SameLengthLists()
        {
            //Arrange 
            var setA = new List<int>() { 1, 2, 3, 4, 5 };
            var setB = new List<int>() { 4, 5, 6, 7, 8 };
            var expectedResult = new List<int>() { 4, 5 };
            //Act
            var actualResult = ArraySetUtilities.Intersection(setA, setB);
            //Assert
            CollectionAssert.AreEqual(actualResult, expectedResult);

        }

        /// <summary>
        /// When called, the expectation is that the expected result of 'setA' and 'setB' distinct list values passed into intersection()
        /// will return one similiar collection elements.
        /// Comparing our expected result of two intersecting terms, to the actual result, should be identical, and thus result in true/pass for
        /// this unit test. 
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIntersection_PartialIntersection2()
        {
            //Arrange 
            var setA = new List<int>() { 1,440,42,45,0,4,45 };
            var setB = new List<int>() { 4, 57, 34, 344, 322, 666, 33 };
            var expectedResult = new List<int>() { 4 };
            //Act
            var actualResult = ArraySetUtilities.Intersection(setA, setB);
            //Assert
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// When called, the expectation is that by passing in non unique values (repeats) of the same numerical value in setA, it will not add
        /// that second value in setA to the list, as it already added itself.
        /// Comparing our expected result, I expect this to pass, if it doesn't check the flag canAddToCommon.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIntersection_PartialIntersection_RepeatsInSetA() //checked
        {
            //Arrange 
            var setA = new List<int>() { 9, 40, 40, 15 };
            var setB = new List<int>() { 10, 40, 20, 5 };
            var expectedResult = new List<int>() { 40 };
            //Act
            var actualResult = ArraySetUtilities.Intersection(setA, setB);
            //Assert
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// When called, the expectation is that by passing in non unique values (repeats) of the same numerical value in setB, it will not add
        /// that value twice to setCommon list, as it already added itself on that passthrough just before.
        /// Comparing our expected result, I expect this to pass, if it doesn't check the flag canAddToCommon.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIntersection_PartialIntersection_RepeatsInSetB() // not check
        {
            //Arrange 
            var setA = new List<int>() { 90, 40, 2000, 1920 };
            var setB = new List<int>() { 9, 90, 90, 1888 };
            var expectedResult = new List<int>() { 90 };
            //Act
            var actualResult = ArraySetUtilities.Intersection(setA, setB);
            //Assert
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// When called, the expectation is that the expected result of 'setA' and 'setB' distinct list values passed into intersection()
        /// will return all similiar terms.
        /// Comparing our expected result, I expect this to pass.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestIntersection_FullIntersection()
        {
            //Arrange 
            var setA = new List<int>() { 111,222,333,444,555,666,777,888 };
            var setB = new List<int>() { 222,111,444,333,666,555,888,777 };
            var expectedResult = new List<int>() { 111,222,333,444,555,666,777,888 };
            //Act
            var actualResult = ArraySetUtilities.Intersection(setA, setB);
            //Assert
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// When called, the expectation is that this method will return true, and thus pass,
        /// under the supposition that setA contains all of setB's values values within the setA provided list,
        /// and that this is the determination of the function of subSet method.
        /// 
        /// 
        /// test negative vaues in b,
        /// mix up the order between a and b
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestSubset_SetALargerThanSubsetB()
        {
            var setA = new List<int>() { 150, 300, -50 , 200 };
            var setB = new List<int>() { 150, 300, -50 };

            var expectedResult = true;

            bool actualResult = ArraySetUtilities.subSet(setA, setB);

            Assert.AreEqual(actualResult,expectedResult);
        }

        /// <summary>
        /// Expectation when called that the subset of B equal to setA; and are equivalent.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestSubset_SetsEquivalent()
        {
            var setA = new List<int>() { 150, 320, -50 };
            var setB = new List<int>() { 150, 320, -50 };

            var expectedResult = true;

            bool actualResult = ArraySetUtilities.subSet(setA, setB);

            Assert.AreEqual(actualResult, expectedResult);
        }
        /// <summary>
        /// The expected result should be false because the 
        /// subset (setB) is larger than the main setA, so the last element
        /// in setB cannot match, as it by default won't exist in setA.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestSubset_SubsetBiggerThanSet()
        {
            var setA = new List<int>() { 40, 185, 32 };
            var setB = new List<int>() { 40, 185, 32, 44 };
            var expectedResult = false;
            bool actualResult = ArraySetUtilities.subSet(setA, setB);

            Assert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// The expected result should be false because the 
        /// because the subsetB strictly to be a subset, must be in the same order
        /// as it appears in setA. However, The last value in each set being equivalent,
        /// determines whether or not the result reported is successful. even if the sets do not match in order,
        /// if the last two digits are equivalent they will be mis-reported as passing.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestSubset_SubsetNotInOrderLastElementEven()
        {
            var setA = new List<int>() { 27, 211, 21};
            var setB = new List<int>() { 211, 27, 21 };
            var expectedResult = false; // should i have set this to true, it would pass
            bool actualResult = ArraySetUtilities.subSet(setA, setB);

            Assert.AreEqual(actualResult, expectedResult);
        }

        /// <summary>
        /// The expectation is false, but not for the reason it does.
        /// It fails because the last element in set A on the last passthrough,
        /// does a compare round first and finds its match then stops. To check for a subset it should
        /// be pairing identically meaning the ! in front of the isInSet shouldn't be there.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void TestSubset_SubsetNotInOrderLastElementOdd()
        {
            var setA = new List<int>() { 28, 214, 25 };
            var setB = new List<int>() { 25, 214, 28 };
            var expectedResult = false;
            bool actualResult = ArraySetUtilities.subSet(setA, setB);

            Assert.AreEqual(actualResult, expectedResult);
        }


        /// <summary>
        /// The expected result should be that the method ends at the exception.
        /// setA has nothing in it to take a subset from.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))] 
        public void TestSubset_SubSetAEmpty()
        {
            var setA = new List<int>();
            var setB = new List<int>() { 40, 185, 32, 44 };
            bool actualResult = ArraySetUtilities.subSet(setA, setB);

            Assert.IsFalse(actualResult);

        }

        /// <summary>
        /// The expected result should be that the method ends at the exception.
        /// subset in setB has nothing in it to compare from A.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSubset_SubSetBEmpty()
        {
            var setA = new List<int>() { 40, 185, 32, 44 };
            var setB = new List<int>();
            bool actualResult = ArraySetUtilities.subSet(setA, setB);
            Assert.IsFalse(actualResult);
        }
    }
}
