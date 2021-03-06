﻿// File name: FileNotFoundExceptionTests.cs
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

#if !(NETSTD10 || NETSTD11)

using NUnit.Framework;
using PommaLabs.Thrower.ExceptionHandlers.IO;
using PommaLabs.Thrower.Reflection;
using Shouldly;
using System;
using System.IO;

namespace PommaLabs.Thrower.UnitTests.ExceptionHandlers.IO
{
    internal sealed class FileNotFoundExceptionTests : AbstractTests
    {
        private static readonly string ExistingFilePath = PortableTypeInfo.GetTypeAssembly<FileNotFoundExceptionTests>().Location;
        private static readonly string NotExistingFilePath = Path.Combine("C:\\", Guid.NewGuid() + ".test");
        private static readonly string MyTestMessage = $"{DateTime.UtcNow} - {Guid.NewGuid()}";

        [Test]
        public void ShouldNotThrowIfFileExists()
        {
            try
            {
                Raise.FileNotFoundException.IfNotExists(ExistingFilePath);
                Raise.FileNotFoundException.IfNotExists(ExistingFilePath, MyTestMessage);
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void ShouldThrowIfFileNotExistsAndShouldUseDefaultMessageIfNoneSpecified()
        {
            try
            {
                Raise.FileNotFoundException.IfNotExists(NotExistingFilePath);
            }
            catch (FileNotFoundException ex)
            {
                ex.Message.ShouldBe(FileNotFoundExceptionHandler.DefaultNotExistsMessage);
                ex.FileName.ShouldBe(NotExistingFilePath);
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void ShouldThrowIfFileNotExistsAndShouldUseCustomMessageIfSpecified()
        {
            try
            {
                Raise.FileNotFoundException.IfNotExists(NotExistingFilePath, MyTestMessage);
            }
            catch (FileNotFoundException ex)
            {
                ex.Message.ShouldBe(MyTestMessage);
                ex.FileName.ShouldBe(NotExistingFilePath);
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}

#endif