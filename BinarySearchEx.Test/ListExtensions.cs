using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ModernRoute.BinarySearchEx.Test
{
    [TestFixture]
    public class ListExtensionsTest
    {
        private void TestBinarySearch(int expectedIndexFirst, int expectedIndexLast,
            int providedValue, params int[] values)
        {
            List<int> list = new List<int>(values);

            Comparer<int> comparer = Comparer<int>.Default;

            list.Sort(comparer);

            Assert.AreEqual(expectedIndexFirst, list.BinarySearchFirst(providedValue, comparer));
            Assert.AreEqual(expectedIndexLast, list.BinarySearchLast(providedValue, comparer));

            list.Sort();

            Assert.AreEqual(expectedIndexFirst, list.BinarySearchFirst(providedValue));
            Assert.AreEqual(expectedIndexLast, list.BinarySearchLast(providedValue));
        }

        [Test]
        public void BinarySearchFirstAgainstNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                List<int> list = null;
                list.BinarySearchFirst(0);
            });
        }

        [Test]
        public void BinarySearchLastAgainstNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                List<int> list = null;
                list.BinarySearchLast(0);
            });
        }

        [Test]
        public void BinarySearchFirstAgainstNullComparer()
        {
            List<int> list = new List<int>();
            Assert.AreEqual(-1, list.BinarySearchFirst(0, null));
        }

        [Test]
        public void BinarySearchLastAgainstNullComparer()
        {
            List<int> list = new List<int>();
            Assert.AreEqual(-1, list.BinarySearchLast(0, null));
        }

        [Test]
        public void BinarySearchAgainstEmptyArray()
        {
            TestBinarySearch(-1, -1, 6);
        }

        [Test]
        public void BinarySearchAgainstOneItemArray()
        {
            TestBinarySearch(0, 0, 4, 4);
        }

        [Test]
        public void BinarySearchNotExistedItemAgainstOneItemArray()
        {
            TestBinarySearch(-2, -2, 5, 3);
        }

        [Test]
        public void BinarySearchAgainstManyItemsArray()
        {
            TestBinarySearch(3, 3, 1394, -10, 50, 128, 1394, 222442342);
        }

        [Test]
        public void BinarySearchFirstItemAgainstManyItemsArray()
        {
            TestBinarySearch(0, 0, -42, -42, 0, 23424353, 234234532, 234234235);
        }

        [Test]
        public void BinarySearchLastItemAgainstManyItemsArray()
        {
            TestBinarySearch(4, 4, 234234635, -42, 0, 23424353, 234234532, 234234635);
        }

        [Test]
        public void BinarySearchAgainstManyItemsArrayWithManyDuplicates()
        {
            TestBinarySearch(1, 4, 23424353, 0, 23424353, 23424353, 23424353, 23424353);
        }

        [Test]
        public void BinarySearchMaxValue()
        {
            TestBinarySearch(4, 4, int.MaxValue, 0, 23424353, 23424353, 23424353, int.MaxValue);
        }

        [Test]
        public void BinarySearchMinValue()
        {
            TestBinarySearch(0, 0, int.MinValue, 0, 23424353, 23424353, 23424353, int.MinValue);
        }

        [Test]
        public void BinarySearchAgainstBigArrayItemNotPresent()
        {
            int[] array = new int[100000];
            for (int i = -50000; i < 50000; i++)
            {
                array[50000 + i] = 2 * i;
            }

            TestBinarySearch(-61713, -61713, 23423, array);
        }

        [Test]
        public void BinarySearchAgainstBigArrayItemPresent()
        {
            int[] array = new int[100000];
            for (int i = -50000; i < 50000; i++)
            {
                array[50000 + i] = 2 * i;
            }

            TestBinarySearch(27339, 27339, -45322, array);
        }

        [Test]
        public void BinarySearchAgainstManyArraySizesWithDuplicatesNoItem()
        {
            Random random = new Random();

            for (int i = 1; i < 100; i++)
            {
                int[] array = new int[2 * i];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = j >> 1 << 1;
                }

                int index = random.Next(i) * 2;

                TestBinarySearch(~(index + 2), ~(index + 2), array[index] + 1, array);

                index = random.Next(i) * 2 + 1;

                TestBinarySearch(~(index + 1), ~(index + 1), array[index] + 1, array);
            }
        }

        [Test]
        public void BinarySearchAgainstManyArraySizesWithDuplicates()
        {
            Random random = new Random();

            for (int i = 1; i < 100; i++)
            {
                int[] array = new int[2 * i];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = j >> 1;
                }

                int index1 = random.Next(i) * 2;
                int index2 = index1 + 1;

                TestBinarySearch(index1, index2, array[index1], array);
            }
        }

        [Test]
        public void BinarySearchAgainstUnsortedArray()
        {
            List<int> array = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                array.Add(~i);
            }

            array.BinarySearchFirst(-3, Comparer<int>.Default);
            array.BinarySearchLast(-3, Comparer<int>.Default);

            array.BinarySearchFirst(-3);
            array.BinarySearchLast(-3);
        }

        [Test]
        public void BinarySearchFirstUncomparableType()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                List<ListExtensionsTest> list = new List<ListExtensionsTest>();
                list.Add(new ListExtensionsTest());

                list.BinarySearchFirst(new ListExtensionsTest());
            });
        }

        [Test]
        public void BinarySearchLastUncomparableType()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                List<ListExtensionsTest> list = new List<ListExtensionsTest>();
                list.Add(new ListExtensionsTest());

                list.BinarySearchLast(new ListExtensionsTest());
            });
        }

        [Test]
        public void BinarySearchFirstIndexLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int[] list = new int[0];
                list.BinarySearchFirst(-1, 0, 3);
            });
        }

        [Test]
        public void BinarySearchLastIndexLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int[] list = new int[0];
                list.BinarySearchLast(-1, 0, 3);
            });
        }

        [Test]
        public void BinarySearchFirstCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int[] list = new int[1] { 3 };
                list.BinarySearchFirst(0, -10, 3);
            });
        }

        [Test]
        public void BinarySearchLastCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int[] list = new int[1] { 3 };
                list.BinarySearchLast(0, -1, 3);
            });
        }

        [Test]
        public void BinarySearchFirstIndexCountDoNotDenote()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                int[] list = new int[1] { 3 };
                list.BinarySearchFirst(0, 2, 3);
            });
        }

        [Test]
        public void BinarySearchLastIndexCountDoNotDenote()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                int[] list = new int[1] { 3 };
                list.BinarySearchLast(0, 2, 3);
            });
        }

        [Test]
        public void BinarySearchFirstNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                int[] list = null;
                list.BinarySearchFirst(0, 2, 3);
            });
        }

        [Test]
        public void BinarySearchLastNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                int[] list = null;
                list.BinarySearchLast(0, 2, 3);
            });
        }

        [Test]
        public void BinarySearchFirstNullWithComparer()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                int[] list = null;
                list.BinarySearchFirst(0, Comparer<int>.Default);
            });
        }

        [Test]
        public void BinarySearchLastNullWithComparer()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                int[] list = null;
                list.BinarySearchLast(0, Comparer<int>.Default);
            });
        }

        [Test]
        public void BinarySearchFirstLast()
        {
            int[] list = new int[3] { 1, 3, 3 };
            Assert.AreEqual(1, list.BinarySearchFirst(0, 3, 3));
            Assert.AreEqual(2, list.BinarySearchLast(0, 3, 3));
        }

        [Test]
        public void BinarySearchLowerItemThanOthersInArray()
        {
            TestBinarySearch(-1, -1, -1, 0, 1, 3, 8, 12);
        }

        [Test]
        public void BinarySearchHigherItemThanOthersInArray()
        {
            TestBinarySearch(-6, -6, 13, 0, 1, 3, 8, 12);
        }
    }
}
