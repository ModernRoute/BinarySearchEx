namespace ModernRoute.BinarySearchEx.Test
{
    [TestFixture]
    public class ListExtensionsTest
    {
        [TestCase(-1, -1, 6, TestName = "EmptyArray")]
        [TestCase(0, 0, 4, 4, TestName = "OneItemArrayExists")]
        [TestCase(-2, -2, 5, 3, TestName = "OneItemArrayDoesNotExist")]
        [TestCase(3, 3, 1394, -10, 50, 128, 1394, 222442342, TestName = "MultipleItemsMiddle")]
        [TestCase(0, 0, -42, -42, 0, 23424353, 234234532, 234234235, TestName = "MultipleItemsFirst")]
        [TestCase(4, 4, 234234635, -42, 0, 23424353, 234234532, 234234635, TestName = "MultipleItemsLast")]
        [TestCase(1, 4, 23424353, 0, 23424353, 23424353, 23424353, 23424353, TestName = "MultipleItemsDuplicates")]
        [TestCase(4, 4, int.MaxValue, 0, 23424353, 23424353, 23424353, int.MaxValue, TestName = "MultipleItemsMaxValue")]
        [TestCase(0, 0, int.MinValue, 0, 23424353, 23424353, 23424353, int.MinValue, TestName = "MultipleItemsMinValue")]
        [TestCase(-1, -1, -1, 0, 1, 3, 8, 12, TestName = "LowerItemThanOthersInArray")]
        [TestCase(-6, -6, 13, 0, 1, 3, 8, 12, TestName = "HigherItemThanOthersInArray")]
        [TestCaseSource(nameof(GetFuzzCases), Category = "FuzzCases")]
        public void CommonCase(int expectedIndexFirst, int expectedIndexLast,
            int providedValue, params int[] values)
        {
            int[] copyValues = new int[values.Length];
            Array.Copy(values, copyValues, values.Length);
            Array.Sort(copyValues);
            Comparer<int> comparer = Comparer<int>.Default;
            Array.Sort(values, comparer);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(values.BinarySearchFirst(providedValue, comparer), Is.EqualTo(expectedIndexFirst));
                Assert.That(values.BinarySearchLast(providedValue, comparer), Is.EqualTo(expectedIndexLast));
                Assert.That(values.BinarySearchFirst(providedValue), Is.EqualTo(expectedIndexFirst));
                Assert.That(values.BinarySearchLast(providedValue), Is.EqualTo(expectedIndexLast));
            }
        }

        [Test]
        public void UnsortedArray()
        {
            List<int> array = [];
            for (int i = 0; i < 5; ++i)
            {
                array.Add(~i);
            }

            array.BinarySearchFirst(-3, Comparer<int>.Default);
            array.BinarySearchLast(-3, Comparer<int>.Default);
            array.BinarySearchFirst(-3);
            array.BinarySearchLast(-3);
        }

        [Test]
        public void UncomparableType()
        {
            List<ListExtensionsTest> list = [new ListExtensionsTest()];
            using (Assert.EnterMultipleScope())
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    list.BinarySearchFirst(new ListExtensionsTest());
                });
                Assert.Throws<InvalidOperationException>(() =>
                {
                    list.BinarySearchLast(new ListExtensionsTest());
                });
            }
        }

        [TestCase(-1, 0, 3, TestName = "IndexLessThenZero")]
        [TestCase(0, -1, 3, 3, TestName = "CountLessThenZero")]
        [TestCase(0, 2, 3, 1, TestName = "IndexCountDoNotDenote")]
        public void OutOfRange(int index, int count, int item, params int[] values)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    values.BinarySearchFirst(index, count, item);
                });
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    values.BinarySearchLast(index, count, item);
                });
            }
        }

        private static IEnumerable<object[]> GetFuzzCases()
        {
            {
                int[] array = GetBigArrayOfEvenIntegersWithoutDuplicates();
                yield return [-61713, -61713, 23423, array];
                yield return [27339, 27339, -45322, array];
            }

            Random random = new();

            for (int i = 1; i < 100; ++i)
            {
                int[] array = GetBigArrayOfEvenUnsignedIntegersWithDuplicates(i);
                int index = random.Next(i) << 1;
                yield return [~(index + 2), ~(index + 2), array[index] + 1, array];
                yield return [~(index + 2), ~(index + 2), array[index + 1] + 1, array];
            }

            for (int i = 1; i < 100; ++i)
            {
                int[] array = GetBigArrayOfUnsignedIntegersWithoutDuplicates(i);
                int index = random.Next(i) << 1;
                yield return [index, index + 1, array[index], array];
            }
        }

        private static int[] GetBigArrayOfEvenIntegersWithoutDuplicates()
        {
            int[] array = new int[100000];
            for (int i = -50000; i < 50000; i++)
            {
                array[50000 + i] = i << 1;
            }

            return array;
        }

        private static int[] GetBigArrayOfEvenUnsignedIntegersWithDuplicates(int halfLength)
        {
            int[] array = new int[2 * halfLength];
            for (int j = 0; j < array.Length; ++j)
            {
                array[j] = j & ~1;
            }
            return array;
        }

        private static int[] GetBigArrayOfUnsignedIntegersWithoutDuplicates(int halfLength)
        {
            int[] array = new int[2 * halfLength];
            for (int j = 0; j < array.Length; ++j)
            {
                array[j] = j >> 1;
            }
            return array;
        }
    }
}
