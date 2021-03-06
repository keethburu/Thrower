﻿// File name: EquatableObject.cs
//
// Author(s): Alessio Parma <alessio.parma@gmail.com>
//
// The MIT License (MIT)
//
// Copyright (c) 2013-2018 Alessio Parma <alessio.parma@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT
// OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace PommaLabs.Thrower.Goodies
{
    /// <summary>
    ///   Implements some common <see cref="object"/> methods, like
    ///   <see cref="IEquatable{T}.Equals(T)"/> and <see cref="object.ToString"/>, so that you don't
    ///   have to copy and paste the same boilerplate code.
    /// </summary>
    /// <typeparam name="T">The type of the object inheriting this class.</typeparam>
    [Serializable]
    public abstract class EquatableObject<T> : FormattableObject, IEquatable<T>
        where T : EquatableObject<T>
    {
        /// <summary>
        ///   Seed used to compute hash code.
        /// </summary>
        public const int HashCodeSeed = 397;

        #region Abstract Methods

        /// <summary>
        ///   Returns all property (or field) values that should be used inside
        ///   <see cref="IEquatable{T}.Equals(T)"/> or <see cref="object.GetHashCode"/>.
        /// </summary>
        /// <returns>
        ///   All property (or field) values that should be used inside
        ///   <see cref="IEquatable{T}.Equals(T)"/> or <see cref="object.GetHashCode"/>.
        /// </returns>
        protected abstract IEnumerable<object> GetIdentifyingMembers();

        #endregion Abstract Methods

        #region Object Methods

        /// <summary>
        ///   Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   true if the current object is equal to the <paramref name="other"/> parameter;
        ///   otherwise, false.
        /// </returns>
        public virtual bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return System.Linq.Enumerable.SequenceEqual(GetIdentifyingMembers(), other.GetIdentifyingMembers());
        }

        /// <summary>
        ///   Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance;
        ///   otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as T);
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and data
        ///   structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return System.Linq.Enumerable.Aggregate(GetIdentifyingMembers(), HashCodeSeed, ComputeHashCode);
            }
        }

        #endregion Object Methods

        #region Equality Operators

        /// <summary>
        ///   Implements the operator ==, by checking equality of left and right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(EquatableObject<T> left, EquatableObject<T> right) => Equals(left, right);

        /// <summary>
        ///   Implements the operator !=, by checking equality of left and right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(EquatableObject<T> left, EquatableObject<T> right) => !Equals(left, right);

        #endregion Equality Operators

        #region Private Methods

        private static int ComputeHashCode(int hashCode, object obj) => (obj == null) ? hashCode : (hashCode ^ obj.GetHashCode());

        #endregion Private Methods
    }
}