using NUnit.Framework;
using SelfBalancingTree;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace TestTree
{
    [TestFixture]
    public class TreeUnitTests
    {
        [Test]
        public void ConstructorShouldCreateNewInstanceOfTreeType_WhenEmptyConstructorIsUsed()
        {
            var actual = new Tree<int>();
            Assert.IsInstanceOf<Tree<int>>(actual);
        }
        [Test]
        public void ConstructorShouldCreateNewInstanceOfTreeType_WhenCollectionIsPassedToTheConstructor()
        {
            var collection = new List<int>() { 0, 1, 2 };

            var actual = new Tree<int>(collection);
            Assert.IsInstanceOf<Tree<int>>(actual);

            foreach (var item in collection)
            {
                Assert.IsTrue(actual.Contains(item));
            }
        }
        [Test]
        public void ConstructorShouldThrowArgumentNullException_WhenNullIsPassedToTheConstructor()
        {
            Assert.Catch<ArgumentNullException>(() => new Tree<int>(null));
        }
        [Test]
        public void ConstructorShouldCreateNewInstanceOfTreeType_WhenItemIsPassedToTheConstructor()
        {
            var number = 42;

            var actual = new Tree<int>(number);
            Assert.IsInstanceOf<Tree<int>>(actual);

            Assert.IsTrue(actual.Contains(number));
        }

        [TestCase(new int[] { 1, 6, 64, 69, 6794, 637, 53, 5, 53679, 58, 568, 5683 })]
        [TestCase(new int[] { 4257, 1299321, 186944, 22236, 8980534, 22 })]
        [TestCase(new int[] { 227227, 536679783, 2172 })]
        [TestCase(new int[] { 1615642, 5389339, 34535, 228, 333333, 3434, 75 })]
        [TestCase(new int[] { 46784, 622, 3919, 29890, 3642832, 7954 })]
        public void TheProperty_Count_ShouldBeValid_WhenUniqueItemsArePassed(int[] inputArray)
        {
            var actual = new Tree<int>(inputArray);

            Assert.AreEqual(actual.Count, inputArray.Length);
        }

        [TestCase(new int[] { 2, 2, 2, 2 })]
        [TestCase(new int[] { 164, 164 })]
        [TestCase(new int[] { 8, 8, 8 })]
        public void TheProperty_Count_ShouldBeOne_WhenNonUniqueItemsArePassed(int[] inputArray)
        {
            var actual = new Tree<int>(inputArray);

            var expected = 1;

            Assert.AreEqual(actual.Count, expected);
        }
        [Test]
        public void TheProperty_Count_ShouldBeZero_WhenThereAreNoElementsInTheTree()
        {
            var actual = new Tree<int>();

            var expected = 0;

            Assert.AreEqual(actual.Count, expected);
        }

        [TestCase(new int[] { 1, 5, 8, 37, 22, 89 }, 89)]
        [TestCase(new int[] { 356, 378, 1111, 5472, 2728, 32, 3 }, 5472)]
        [TestCase(new int[] { 978, 22, 455, 3346, 323, 673, 3333 }, 3346)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 9)]
        [TestCase(new int[] { 9, 8, 7, 6, 5, 4, 3 }, 9)]
        public void TheProperty_MaxValue_ShouldBeValid_WhenAdding(int[] inputArray, int expected)
        {
            var tree = new Tree<int>(inputArray);

            var actual = tree.MaxValue;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(50)]
        [TestCase(60)]
        [TestCase(80)]
        public void TheProperty_MaxValue_ShouldBeValid_WhenAddingAndRemoving(int numberOfOperations)
        {
            var tree = new Tree<int>();
            var dictionary = new SortedSet<int>();

            Random rnd = new Random();

            for (int i = 0; i < numberOfOperations; i++)
            {
                var currentValue = rnd.Next(-200, 200);
                tree.Add(currentValue);
                dictionary.Add(currentValue);
            }

            tree.Add(-50);
            dictionary.Add(-50);

            for (int i = 0; i < numberOfOperations * 5; i++)
            {
                var currentValue = rnd.Next(0, 200);
                tree.Remove(currentValue);
                dictionary.Remove(currentValue);
            }

            var expected = dictionary.Max;

            var actual = tree.MaxValue;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TheProperty_MaxValue_ShouldThrowArgumentNullException_WhenTheTreeIsEmpty()
        {
            var tree = new Tree<string>();

            Assert.Catch<ArgumentNullException>(() =>
            {
                var exeption = tree.MaxValue;
            });
        }

        [TestCase(new int[] { 1, 5, 8, 37, 22, 89 }, 1)]
        [TestCase(new int[] { 356, 378, 1111, 5472, 2728, 32, 3 }, 3)]
        [TestCase(new int[] { 978, 22, 455, 3346, 323, 673, 3333 }, 22)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 1)]
        [TestCase(new int[] { 9, 8, 7, 6, 5, 4, 3 }, 3)]
        public void TheProperty_MinValue_ShouldBeValid_WhenAdding(int[] inputArray, int expected)
        {
            var tree = new Tree<int>(inputArray);

            var actual = tree.MinValue;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(50)]
        [TestCase(60)]
        [TestCase(80)]
        public void TheProperty_MinValue_ShouldBeValid_WhenAddingAndRemoving(int numberOfOperations)
        {
            var tree = new Tree<int>();
            var dictionary = new SortedSet<int>();

            Random rnd = new Random();

            for (int i = 0; i < numberOfOperations; i++)
            {
                var currentValue = rnd.Next(-200, 200);
                tree.Add(currentValue);
                dictionary.Add(currentValue);
            }

            tree.Add(50);
            dictionary.Add(50);

            for (int i = 0; i < numberOfOperations * 5; i++)
            {
                var currentValue = rnd.Next(-200, 0);
                tree.Remove(currentValue);
                dictionary.Remove(currentValue);
            }

            var expected = dictionary.Min;

            var actual = tree.MinValue;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TheProperty_MinValue_ShouldThrowArgumentNullException_WhenTheTreeIsEmpty()
        {
            var tree = new Tree<string>();

            Assert.Catch<ArgumentNullException>(() =>
            {
                var exeption = tree.MinValue;
            });
        }

        [Test]
        public void TheProperty_IsReadOnly_ShouldReturnFalse()
        {
            var tree = new Tree<string>();

            var actual = tree.IsReadOnly;

            var expected = false;

            Assert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        [TestCase(-56)]
        [TestCase(90)]
        [TestCase(55)]
        public void Add_ShouldReturnTrue_WhenUniqueValueIsPassed(int value)
        {
            var tree = new Tree<int>();

            var actual = tree.Add(value);

            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(-56)]
        [TestCase(90)]
        [TestCase(55)]
        public void Add_ShouldReturnFalse_WhenNonUniqueValueIsPassed(int value)
        {
            var tree = new Tree<int>();

            tree.Add(value);
            var actual = tree.Add(value);

            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(-56)]
        [TestCase(90)]
        [TestCase(55)]
        public void Remove_ShouldReturnTrue_WhenTryingToRemoveValueThatIsInTheCollection(int value)
        {
            var tree = new Tree<int>();

            tree.Add(value);
            var actual = tree.Remove(value);

            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestCase((uint)20)]
        [TestCase((uint)55)]
        [TestCase((uint)40)]
        [TestCase((uint)30)]
        public void Remove_ShouldReturnFalse_WhenTryingToRemoveValueThatIsNotInTheCollection(uint value)
        {
            var tree = new Tree<uint>();
            value++;

            for (uint i = 0; i < value * 2; i++)
            {
                tree.Add(i);
            }
            tree.Remove(value);

            var actual = tree.Remove(value);

            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Remove_ShouldReturnFalse_WhenTheCollectionIsEmpty()
        {
            var tree = new Tree<uint>();

            var actual = tree.Remove(10);

            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestCase((uint)10)]
        [TestCase((uint)78)]
        [TestCase((uint)90)]
        [TestCase((uint)12)]
        public void Contains_ShouldReturnTrue_WhenTheCollectionContainsTheValueThatWeSeek(uint value)
        {
            var tree = new Tree<uint>();
            value++;

            for (uint i = 0; i < value * 2; i++)
            {
                tree.Add(i);
            }

            var actual = tree.Contains(value);

            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestCase((uint)10)]
        [TestCase((uint)78)]
        [TestCase((uint)90)]
        [TestCase((uint)12)]
        public void Contains_ShouldReturnFalse_WhenTheCollectionDoesNotContainsTheValueThatWeSeek(uint value)
        {
            var tree = new Tree<uint>();
            value++;

            for (uint i = 0; i < value * 2; i++)
            {
                tree.Add(i);
            }
            tree.Remove(value);

            var actual = tree.Contains(value);

            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Contains_ShouldReturnFalse_WhenTheTreeIsEmpty()
        {
            var tree = new Tree<int>();

            var actual = tree.Contains(9);

            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestCase((uint)40)]
        [TestCase((uint)60)]
        [TestCase((uint)20)]
        public void Clear_ShouldRemoveAllElementsInTheCollection(uint value)
        {
            var tree = new Tree<uint>();

            for (uint i = 0; i < value; i++)
            {
                tree.Add(i);
            }

            tree.Clear();

            var expected = 0;

            Assert.AreEqual(expected, tree.Count);
        }

        [TestCase(4)]
        [TestCase(10)]
        [TestCase(20)]
        public void CopyTo_WithOneParameter_ShouldAddAllElementsFromTheTreeToTheInputArray_WhenCorrectParametersArePassed(int value)
        {
            var tree = new Tree<int>();
            var array = new int[value];

            for (int i = 0; i < value; i++)
            {
                tree.Add(i);
            }

            tree.CopyTo(array);

            var counter = 0;

            foreach (var item in tree)
            {
                Assert.AreEqual(array[counter], item);
                counter++;
            }
        }

        [Test]
        public void CopyTo_WithOneParameter_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.CopyTo(null));
        }

        [TestCase(41)]
        [TestCase(31)]
        [TestCase(11)]
        public void CopyTo_WithOneParameter_ShouldThrowArgumentException_WhenAnArraySmallerThenTheCollectionIsPassed(int value)
        {
            var tree = new Tree<int>();
            var array = new int[value];

            for (int i = 0; i < value + 1; i++)
            {
                tree.Add(i);
            }

            Assert.Catch<ArgumentException>(() => tree.CopyTo(array));
        }

        [TestCase(4, 5)]
        [TestCase(10, 3)]
        [TestCase(20, 12)]
        public void CopyTo_WithTwoParameter_ShouldAddAllElementsFromTheTreeToTheInputArray_WhenCorrectParametersArePassed(int value, int startIndex)
        {
            var tree = new Tree<int>();
            var array = new int[value + startIndex];

            for (int i = 0; i < value; i++)
            {
                tree.Add(i);
            }

            tree.CopyTo(array, startIndex);

            var counter = startIndex;

            foreach (var item in tree)
            {
                Assert.AreEqual(array[counter], item);
                counter++;
            }
        }

        [Test]
        public void CopyTo_WithTwoParameter_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.CopyTo(null, 22));
        }

        [TestCase(41, 4)]
        [TestCase(31, 20)]
        [TestCase(11, 11)]
        public void CopyTo_WithTwoParameter_ShouldThrowArgumentException_WhenAnArraySmallerThenTheCollectionIsPassed(int value, int startIndex)
        {
            var tree = new Tree<int>();
            var array = new int[value + startIndex];

            for (int i = 0; i < value + 1; i++)
            {
                tree.Add(i);
            }

            Assert.Catch<ArgumentException>(() => tree.CopyTo(array, startIndex));
        }

        [TestCase(0)]
        [TestCase(14)]
        [TestCase(34)]
        [TestCase(42)]
        public void Reverse_ShouldReturnReversedCollectionOfThisTree_WhenCalled(int value)
        {
            var tree = new Tree<int>();
            var expected = new List<int>();

            for (int i = value - 1; i >= 0; i--)
            {
                expected.Add(i);
            }

            for (int i = 0; i < value; i++)
            {
                tree.Add(i);
            }

            var actual = tree.Reverse();

            var counter = 0;
            foreach (var item in actual)
            {
                Assert.AreEqual(expected[counter], item);
                counter++;
            }
        }

        [TestCase(0)]
        [TestCase(14)]
        [TestCase(34)]
        [TestCase(42)]
        public void ToString_ShouldReturnStringWithAllElements_WhereEachElementHasCalledTheirToString_WhenCalled(int value)
        {
            var tree = new Tree<int>();
            StringBuilder expected = new StringBuilder();

            for (int i = 0; i < value - 1; i++)
            {
                tree.Add(i);
                expected.Append(i.ToString() + ", ");
            }
            tree.Add(value);
            expected.Append(value.ToString() + "!");

            var actual = tree.ToString();

            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestCase(0)]
        [TestCase(14)]
        [TestCase(34)]
        [TestCase(42)]
        public void ToHashSet_ShouldReturnHashSetThatContainsAllElementsInTheTree(int value)
        {
            var tree = new Tree<int>();
            var expected = new HashSet<int>();

            for (int i = 0; i < value; i++)
            {
                expected.Add(i);
                tree.Add(i);
            }

            var actual = tree.ToHashSet();

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var item in expected)
            {
                Assert.IsTrue(tree.Contains(item));
            }
        }

        [TestCase(0)]
        [TestCase(14)]
        [TestCase(34)]
        [TestCase(42)]
        public void ToList_ShouldReturnListThatContainsAllElementsInTheTree(int value)
        {
            var tree = new Tree<int>();
            var expected = new List<int>();

            for (int i = 0; i < value; i++)
            {
                expected.Add(i);
                tree.Add(i);
            }

            var actual = tree.ToList();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestCase(1)]
        [TestCase(14)]
        [TestCase(34)]
        [TestCase(42)]
        public void DisableSelfBalancing_ShouldDisableSelfBalancing_WhenCalled(int value)
        {
            var tree = new Tree<int>();
            var expected = value - 1;

            tree.DisableSelfBalancing();

            for (int i = 0; i < value; i++)
            {
                tree.Add(i);
            }

            var actual = (tree.GetTopLevels()).ToList()[0];

            Assert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(14)]
        [TestCase(34)]
        [TestCase(42)]
        public void EnableSelfBalancing_ShouldEnableSelfBalancing_WhenCalled(int value)
        {
            var tree = new Tree<int>(false);
            const int minNumberOfElements = 3;

            tree.EnableSelfBalancing();

            for (int i = 0; i < value + minNumberOfElements; i++)
            {
                tree.Add(i);
            }

            Assert.IsTrue(tree.GetTopLevels().ToList().Count > 1);
        }

        [TestCase(0, new int[] { })]
        [TestCase(1, new int[] { 0 })]
        [TestCase(4, new int[] { 1, 2 })]
        [TestCase(6, new int[] { 2, 2, 2 })]
        [TestCase(8, new int[] { 2, 2, 3, 3 })]
        [TestCase(10, new int[] { 2, 2, 3, 3, 3 })]
        public void GetTopLevels_ShouldReturnCollectionOfTheLevelsOfAllTopLevelElements(int value, int[] expected)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < value; i++)
            {
                tree.Add(i);
            }

            var actual = tree.GetTopLevels();

            var counter = 0;

            foreach (var item in actual)
            {
                Assert.AreEqual(expected[counter], item);
                counter++;
            }
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 4, 7, 83, 33, 8 })]
        [TestCase(new int[] { 11, 472, 837, 8383, 933, 448 })]
        [TestCase(new int[] { 14, 4222, 447, 8333, 3388, 38 })]
        public void UnionWith_ShouldAddTheElementsinTheCollectionToTheTree_WhenValidParamsArePassed(int[] expected)
        {
            var actual = new Tree<int>();

            actual.UnionWith(expected);

            Assert.AreEqual(actual.Count, expected.Length);

            foreach (var item in actual)
            {
                Assert.IsTrue(expected.Contains(item));
            }
        }

        [Test]
        public void UnionWith_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.UnionWith(null));
        }

        [Test]
        public void IntersectWith_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.IntersectWith(null));
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 4, 7, 83, 33, 8 })]
        [TestCase(new int[] { 11, 472, 837, 8383, 933, 448 })]
        [TestCase(new int[] { 14, 4222, 447, 8333, 3388, 38 })]
        public void IntersectWith_ShouldModifyTheTree_InSuchWayThatItContainsOnlyTheElementsThatAreInTheInputCollection(int[] expected)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 100; i++)
            {
                tree.Add(i);
            }

            tree.IntersectWith(expected);

            Assert.AreEqual(tree.Count, expected.Length);

            foreach (var item in tree)
            {
                Assert.IsTrue(expected.Contains(item));
            }
        }

        [Test]
        public void ExceptWith_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.ExceptWith(null));
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 4, 7, 83, 33, 8 })]
        [TestCase(new int[] { 11, 42, 837, 8383, 933, 448 })]
        [TestCase(new int[] { 14, 4222, 447, 8333, 3388, 38 })]
        public void ExceptWith_ShouldRemove_FromTheTree_AllElementsThatAreInTheInputCollection(int[] expected)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 100; i++)
            {
                tree.Add(i);
            }

            tree.ExceptWith(expected);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsFalse(tree.Contains(expected[i]));
            }
        }

        [Test]
        public void SymmetricExceptWith_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.SymmetricExceptWith(null));
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 4, 7, 83, 33, 8 })]
        [TestCase(new int[] { 11, 42, 837, 8383, 933, 448 })]
        [TestCase(new int[] { 14, 4222, 447, 8333, 3388, 38 })]
        public void SymmetricExceptWith_ShouldRemoveTheItemsThatAreInBothOfTheCollections(int[] inputArray)
        {
            var tree = new Tree<int>();
            var itemsThatShouldBeRemovedFromTheTree = new List<int>();
            var itemsThatShouldBeAdded = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                tree.Add(i);
            }

            foreach (var item in inputArray)
            {
                if (tree.Contains(item))
                {
                    itemsThatShouldBeRemovedFromTheTree.Add(item);
                }
                else
                {
                    itemsThatShouldBeAdded.Add(item);
                }
            }

            tree.SymmetricExceptWith(inputArray);

            foreach (var item in itemsThatShouldBeAdded)
            {
                Assert.IsTrue(tree.Contains(item));
            }

            foreach (var item in itemsThatShouldBeRemovedFromTheTree)
            {
                Assert.IsFalse(tree.Contains(item));
            }
        }
        [Test]
        public void IsSubsetOf_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.IsSubsetOf(null));
        }

        [Test]
        public void IsSubsetOf_ShouldReturnFalse_WhenTheTreeIsBiggerThenTheInputArray()
        {
            var tree = new Tree<int>();
            var expected = new int[] { 1, 6, 8, 3 };

            for (int i = 0; i < 5; i++)
            {
                tree.Add(i);
            }

            Assert.IsFalse(tree.IsSubsetOf(expected));
        }

        [TestCase(new int[] { 1, 4, 6, 2 })]
        [TestCase(new int[] { 4, 5, 6, 7 })]
        [TestCase(new int[] { 7, 8, 5, 1, 3, 5, 9 })]
        public void IsSubsetOf_ShouldReturnFalse_WhenTheTreeIsNotSubsetOfTheCollection(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 3; i++)
            {
                tree.Add(i);
            }

            var expected = false;

            Assert.AreEqual(expected, tree.IsSubsetOf(inputArray));
        }

        [TestCase(new int[] { 9, 4, 0, 1, 2, 6, 2 })]
        [TestCase(new int[] { 0, 1, 2 })]
        [TestCase(new int[] { 9, 55, 0, 1, 2 })]
        [TestCase(new int[] { 0, 1, 2, 7, 4 })]
        public void IsSubsetOf_ShouldReturnTrue_WhenTheTreeIsSubsetOfTheCollection(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 3; i++)
            {
                tree.Add(i);
            }

            var expected = true;

            Assert.AreEqual(expected, tree.IsSubsetOf(inputArray));
        }

        [Test]
        public void IsSupersetOf_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.IsSupersetOf(null));
        }

        [Test]
        public void IsSupersetOf_ShouldReturnFalse_WhenTheCollectionIsBiggerThenTheTree()
        {
            var tree = new Tree<int>();
            var expected = new int[] { 1, 6, 8, 3, 7, 44 };

            for (int i = 0; i < 5; i++)
            {
                tree.Add(i);
            }

            Assert.IsFalse(tree.IsSupersetOf(expected));
        }

        [TestCase(new int[] { 1, 4, 6, 2 })]
        [TestCase(new int[] { 4, 22, 6, 7 })]
        [TestCase(new int[] { 7, 8, 5, 1, 3, 5, 9 })]
        public void IsSupersetOf_ShouldReturnFalse_WhenTheCollectionIsNotSubsetOfTheTree(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 20; i++)
            {
                tree.Add(i);
            }

            var expected = false;

            Assert.AreEqual(expected, tree.IsSupersetOf(inputArray));
        }

        [TestCase(new int[] { 2, 3, 4 })]
        [TestCase(new int[] { 0, 1, 2 })]
        [TestCase(new int[] { 7 })]
        [TestCase(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })]
        public void IsSupersetOf_ShouldReturnTrue_WhenTheCollectionIsSubsetOfTheTree(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 20; i++)
            {
                tree.Add(i);
            }

            var expected = true;

            Assert.AreEqual(expected, tree.IsSupersetOf(inputArray));
        }

        [Test]
        public void IsProperSupersetOf_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.IsProperSupersetOf(null));
        }

        [Test]
        public void IsProperSupersetOf_ShouldReturnFalse_WhenTheCollectionIsEqualOrBiggerThenTheTree()
        {
            var tree = new Tree<int>();
            var expected = new int[] { 1, 6, 8, 3, 7 };

            for (int i = 0; i < 5; i++)
            {
                tree.Add(i);
            }

            Assert.IsFalse(tree.IsProperSupersetOf(expected));
        }

        [TestCase(new int[] { 1, 4, 6, 2 })]
        [TestCase(new int[] { 4, 22, 6, 7 })]
        [TestCase(new int[] { 7, 8, 5, 1, 3, 5, 9 })]
        public void IsProperSupersetOf_ShouldReturnFalse_WhenTheCollectionIsNotProperSubsetOfTheTree(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 20; i++)
            {
                tree.Add(i);
            }

            var expected = false;

            Assert.AreEqual(expected, tree.IsProperSupersetOf(inputArray));
        }

        [TestCase(new int[] { 2, 17, 18 })]
        [TestCase(new int[] { 0, 1, 2 })]
        [TestCase(new int[] { 7 })]
        [TestCase(new int[] { 0, 1, 2, 3, 9, 10, 15, 16, 17, 18, 19 })]
        public void IsProperSupersetOf_ShouldReturnTrue_WhenTheCollectionIsProperSubsetOfTheTree(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 20; i++)
            {
                tree.Add(i);
            }

            var expected = true;

            Assert.AreEqual(expected, tree.IsProperSupersetOf(inputArray));
        }

        [Test]
        public void IsProperSubsetOf_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.IsProperSubsetOf(null));
        }

        [Test]
        public void IsProperSubsetOf_ShouldReturnFalse_WhenTheTreeIsEqualOrBiggerTheTheCollection()
        {
            var tree = new Tree<int>();

            var array = new int[] { 0, 1, 2, 3, 4 };

            for (int i = 0; i < 5; i++)
            {
                tree.Add(i);
            }

            Assert.IsFalse(tree.IsProperSubsetOf(array));
        }
        [TestCase(new int[] { 23, 56, 23, 1, 7 })]
        [TestCase(new int[] { 1, 44, 2, 6 })]
        [TestCase(new int[] { 0, 4, 1, 7 })]
        [TestCase(new int[] { 22, 0, 1, 3, 5 })]
        public void IsProperSubsetOf_ShouldReturnFalse_WhenTheTreeIsNotProperSubsetOfTheCollection(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 3; i++)
            {
                tree.Add(i);
            }

            Assert.IsFalse(tree.IsProperSubsetOf(inputArray));
        }

        [TestCase(new int[] { 0, 1, 2, 3, 1, 7 })]
        [TestCase(new int[] { 0, 1, 44, 2, 6 })]
        [TestCase(new int[] { 0, 4, 1, 7, 2 })]
        [TestCase(new int[] { 22, 0, 1, 3, 2, 5 })]
        public void IsProperSubsetOf_ShouldReturnTrue_WhenTheTreeIsProperSubsetOfTheCollection(int[] inputArray)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 3; i++)
            {
                tree.Add(i);
            }

            Assert.IsTrue(tree.IsProperSubsetOf(inputArray));
        }

        [Test]
        public void Overlaps_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.Overlaps(null));
        }

        [TestCase(new int[] { 22, 114, 262, 333, 3332 })]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 444, 151, 166, -135 })]
        [TestCase(new int[] { -4, -111 })]
        public void Overlaps_ShouldReturnFalse_WhenTheTreeAndTheCollectionDontShareAnyElements(int[] input)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 20; i++)
            {
                tree.Add(i);
            }

            Assert.IsFalse(tree.Overlaps(input));
        }

        [TestCase(new int[] { 22, 114, 2, 333, 3332 })]
        [TestCase(new int[] { 2 })]
        [TestCase(new int[] { 444, 151, 6, -135 })]
        [TestCase(new int[] { 4, -111 })]
        public void Overlaps_ShouldReturnTrue_WhenTheTreeAndTheCollectionShareAtLeastOneElements(int[] input)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 20; i++)
            {
                tree.Add(i);
            }

            Assert.IsTrue(tree.Overlaps(input));
        }
        [Test]
        public void SetEquals_ShouldThrowArgumentNullException_WhenNullIsPassed()
        {
            var tree = new Tree<int>();

            Assert.Catch<ArgumentNullException>(() => tree.SetEquals(null));
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 2, 3 })]
        public void SetEquals_ShouldReturnFalse_WhenTheSizeOfTheTwoCollectionsIsDifferent(int[] input)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 3; i++)
            {
                tree.Add(i);
            }

            Assert.IsFalse(tree.SetEquals(input));
        }
        [TestCase(new int[] { 1, 2, 3, 0 })]
        [TestCase(new int[] { 2, 3, 0, 1 })]
        [TestCase(new int[] { 3, 2, 1, 0 })]
        public void SetEquals_ShouldReturnTrue_WhenTheTwoCollectionsContainsTheSameElements(int[] input)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 4; i++)
            {
                tree.Add(i);
            }

            Assert.IsTrue(tree.SetEquals(input));
        }

        [Test]
        public void DeepCopyOfThisTree_ShouldThrowSerializationException_WhenTypeIsNotSerializable()
        {
            var tree = new Tree<TestClass>();

            for (int i = 0; i < 10; i++)
            {
                var element = new TestClass();
                tree.Add(element);
            }

            Assert.Catch<SerializationException>(() => tree.DeepCopyOfThisTree());
        }
        [Test]
        public void DeepCopyOfThisTree_ShouldReturnDeepCopyOfTheTree_WhenTypeIsSerializable()
        {
            var tree = new Tree<int>();

            for (int i = 0; i < 10; i++)
            {
                tree.Add(i);
            }

            var newTree = tree.DeepCopyOfThisTree();

            Assert.AreNotSame(newTree, tree);

            newTree.Add(42);

            Assert.IsTrue(newTree.Count > tree.Count);
        }
    }
}
