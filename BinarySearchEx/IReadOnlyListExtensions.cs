
namespace ModernRoute.BinarySearchEx
{
    /// <summary>
    /// Extensions for <see cref="IReadOnlyList&lt;T&gt;"/>
    /// </summary>
    public static class IReadOnlyListExtensions
    {
        /// <summary>
        /// Searches the entire sorted <see cref="IReadOnlyList&lt;T&gt;"/> for an element
        /// using the specified comparer and returns the nearest to to beginning zero-based
        /// index of the element.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The sorted <see cref="IReadOnlyList&lt;T&gt;"/> to search</param>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <param name="comparer">
        /// <p>The <see cref="IComparer&lt;T&gt;"/> implementation to use when comparing elements.</p>
        /// <p>-or-</p>
        /// <p><b>null</b> to use the default comparer <see cref="Comparer&lt;T&gt;.Default"/>.</p>
        /// </param>
        /// <returns>
        /// The nearest to beginning zero-based index of item in the sorted <see cref="IReadOnlyList&lt;T&gt;"/>,
        /// if <paramref name="item"/> is found; otherwise, a negative number that is the bitwise complement
        /// of the index of the next element that is larger than <paramref name="item"/> or, if there is no
        /// larger element, the bitwise complement of <see cref="IReadOnlyList&lt;T&gt;.Count"/>.
        /// </returns>
        public static int BinarySearchFirst<T>(
            this IReadOnlyList<T> array, T item, IComparer<T>? comparer = null)
        {
            return array.BinarySearchFirst(0, array.Count, item, comparer);
        }

        /// <summary>
        /// Searches the entire sorted <see cref="IReadOnlyList&lt;T&gt;"/> for an element
        /// using the specified comparer and returns the nearest to ending zero-based
        /// index of the element.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The sorted <see cref="IReadOnlyList&lt;T&gt;"/> to search</param>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <param name="comparer">
        /// <p>The <see cref="IComparer&lt;T&gt;"/> implementation to use when comparing elements.</p>
        /// <p>-or-</p>
        /// <p><b>null</b> to use the default comparer <see cref="Comparer&lt;T&gt;.Default"/>.</p>
        /// </param>
        /// <returns>
        /// The nearest to ending zero-based index of item in the sorted <see cref="IReadOnlyList&lt;T&gt;"/>,
        /// if <paramref name="item"/> is found; otherwise, a negative number that is the bitwise complement
        /// of the index of the next element that is larger than <paramref name="item"/> or, if there is no
        /// larger element, the bitwise complement of <see cref="IReadOnlyList&lt;T&gt;.Count"/>.
        /// </returns>
        public static int BinarySearchLast<T>(
            this IReadOnlyList<T> array, T item, IComparer<T>? comparer = null)
        {
            return array.BinarySearchLast(0, array.Count, item, comparer);
        }

        /// <summary>
        /// Searches a range of elements in the sorted <see cref="IReadOnlyList&lt;T&gt;"/> for an
        /// element using the specified comparer and returns the nearest to beginning
        /// zero-based index of the element.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The sorted <see cref="IReadOnlyList&lt;T&gt;"/> to search</param>
        /// <param name="index">The zero-based starting index of the range to search.</param>
        /// <param name="count">The length of the range to search.</param>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <param name="comparer">
        /// <p>The <see cref="IComparer&lt;T&gt;"/> implementation to use when comparing elements.</p>
        /// <p>-or-</p>
        /// <p><b>null</b> to use the default comparer <see cref="Comparer&lt;T&gt;.Default"/>.</p>
        /// </param>
        /// <returns>
        /// The nearest to beginning zero-based index of item in the sorted <see cref="IReadOnlyList&lt;T&gt;"/>,
        /// if <paramref name="item"/> is found; otherwise, a negative number that is the bitwise complement
        /// of the index of the next element that is larger than <paramref name="item"/> or, if there is no
        /// larger element, the bitwise complement of <see cref="IReadOnlyList&lt;T&gt;.Count"/>.
        /// </returns>
        public static int BinarySearchFirst<T>(
            this IReadOnlyList<T> array, int index, int count, T item, IComparer<T>? comparer = null)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfLessThan(count, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index + count - 1, array.Count);
            comparer ??= Comparer<T>.Default;

            try
            {
                int left = index;
                int right = index + count - 1;

                if (left > right)
                {
                    return ~index;
                }

                while (right - left > 0)
                {
                    int middle = left + (right - left) / 2;

                    if (comparer.Compare(item, array[middle]) > 0)
                    {
                        left = middle + 1;
                    }
                    else
                    {
                        right = middle;
                    }
                }

                int res = comparer.Compare(item, array[left]);

                if (res > 0)
                {
                    return ~(left + 1);
                }
                else if (res < 0)
                {
                    return ~left;
                }
                else
                {
                    return left;
                }
            }
            catch (Exception ex) when (!IsCritical(ex))
            {
                throw new InvalidOperationException("Failed to compare two elements.", ex);
            }
        }

        /// <summary>
        /// Searches a range of elements in the sorted <see cref="IReadOnlyList&lt;T&gt;"/> for an
        /// element using the specified comparer and returns the nearest to ending
        /// zero-based index of the element.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The sorted <see cref="IReadOnlyList&lt;T&gt;"/> to search</param>
        /// <param name="index">The zero-based starting index of the range to search.</param>
        /// <param name="count">The length of the range to search.</param>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <param name="comparer">
        /// <p>The <see cref="IComparer&lt;T&gt;"/> implementation to use when comparing elements.</p>
        /// <p>-or-</p>
        /// <p><b>null</b> to use the default comparer <see cref="Comparer&lt;T&gt;.Default"/>.</p>
        /// </param>
        /// <returns>
        /// The nearest to ending zero-based index of item in the sorted <see cref="IReadOnlyList&lt;T&gt;"/>,
        /// if <paramref name="item"/> is found; otherwise, a negative number that is the bitwise complement
        /// of the index of the next element that is larger than <paramref name="item"/> or, if there is no
        /// larger element, the bitwise complement of <see cref="IReadOnlyList&lt;T&gt;.Count"/>.
        /// </returns>
        public static int BinarySearchLast<T>(
            this IReadOnlyList<T> array, int index, int count, T item, IComparer<T>? comparer = null)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfLessThan(count, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index + count - 1, array.Count);
            comparer ??= Comparer<T>.Default;

            try
            {
                int left = index;
                int right = index + count - 1;

                if (left > right)
                {
                    return ~index;
                }

                while (right - left > 0)
                {
                    int middle = right - (right - left) / 2;

                    if (comparer.Compare(item, array[middle]) < 0)
                    {
                        right = middle - 1;
                    }
                    else
                    {
                        left = middle;
                    }
                }

                int res = comparer.Compare(item, array[right]);

                if (res > 0)
                {
                    return ~(right + 1);
                }
                else if (res < 0)
                {
                    return ~right;
                }
                else
                {
                    return right;
                }
            }
            catch (Exception ex) when (!IsCritical(ex))
            {
                throw new InvalidOperationException("Failed to compare two elements.", ex);
            }
        }

        private static bool IsCritical(Exception ex)
        {
            return ex is OutOfMemoryException ||
                   ex is AppDomainUnloadedException ||
                   ex is BadImageFormatException ||
                   ex is CannotUnloadAppDomainException ||
                   ex is InvalidProgramException ||
                   ex is ThreadAbortException;
        }
    }
}
