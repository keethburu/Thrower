﻿// File name: RaiseArgumentNullExceptionTests.cs
//
// Author(s): Alessio Parma <alessio.parma@gmail.com>
//
// The MIT License (MIT)
//
// Copyright (c) 2013-2016 Alessio Parma <alessio.parma@gmail.com>
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

using NUnit.Framework;
using System;

namespace PommaLabs.Thrower.UnitTests
{
    internal sealed class RaiseArgumentNullExceptionTests : AbstractTests
    {
        [Test]
        public void NotNullArgument_String()
        {
            RaiseArgumentNullException.IfIsNull("PINO");
        }

        [Test]
        public void NotNullArgument_String_WithMsg()
        {
            RaiseArgumentNullException.IfIsNull("PINO", "PINO", "GINO");
        }

        [Test]
        public void NotNullArgument_Struct()
        {
            RaiseArgumentNullException.IfIsNull(37M);
        }

        [Test]
        public void NotNullArgument_Struct_WithArgName()
        {
            RaiseArgumentNullException.IfIsNull(37M, "DECIMAL");
        }

        [Test]
        public void NotNullArgument_Struct_WithMsg()
        {
            RaiseArgumentNullException.IfIsNull(37M, "DECIMAL", "GINO");
        }

        [Test]
        public void NotNullArgument_BoxedStruct()
        {
            object box = 37M;
            RaiseArgumentNullException.IfIsNull(box);
        }

        [Test]
        public void NotNullArgument_BoxedStruct_WithMsg()
        {
            object box = 37M;
            RaiseArgumentNullException.IfIsNull(box, "DECIMAL", "GINO");
        }

        [Test]
        public void NotNullArgument_BoxedNullableInt_WithValue()
        {
            object box = new int?(21);
            RaiseArgumentNullException.IfIsNull(box);
        }

        [Test]
        public void NotNullArgument_BoxedNullableInt_WithValue_WithArgName()
        {
            object box = new int?(21);
            RaiseArgumentNullException.IfIsNull(box, "null");
        }

        [Test]
        public void NotNullArgument_BoxedNullableInt_WithValue_WithMsg()
        {
            object box = new int?(21);
            RaiseArgumentNullException.IfIsNull(box, "null", TestMessage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgument_NullObject()
        {
            RaiseArgumentNullException.IfIsNull((object) null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = TestMessage, MatchType = MessageMatch.StartsWith)]
        public void NullArgument_NullObject_WithMsg()
        {
            RaiseArgumentNullException.IfIsNull((object) null, "null", TestMessage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgument_NullableInt_WithoutValue()
        {
            RaiseArgumentNullException.IfIsNull(new int?());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgument_NullableInt_WithoutValue_WithArgName()
        {
            RaiseArgumentNullException.IfIsNull(new int?(), "null");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = TestMessage, MatchType = MessageMatch.StartsWith)]
        public void NullArgument_NullableInt_WithoutValue_WithMsg()
        {
            RaiseArgumentNullException.IfIsNull(new int?(), "null", TestMessage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgument_BoxedNullableInt_WithoutValue()
        {
            object box = new int?();
            RaiseArgumentNullException.IfIsNull(box);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgument_BoxedNullableInt_WithoutValue_WithArgName()
        {
            object box = new int?();
            RaiseArgumentNullException.IfIsNull(box, "null");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = TestMessage, MatchType = MessageMatch.StartsWith)]
        public void NullArgument_BoxedNullableInt_WithoutValue_WithMsg()
        {
            object box = new int?();
            RaiseArgumentNullException.IfIsNull(box, "null", TestMessage);
        }
    }
}