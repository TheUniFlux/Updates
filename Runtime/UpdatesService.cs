/*
Copyright (c) 2023 Xavier Arpa López Thomas Peter ('Kingdox')

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kingdox.UniFlux;
namespace Kingdox.UniFlux.Updates
{
    public static partial class UpdatesService // Data
    {
        private static partial class Data
        {

        }
    }
    public static partial class UpdatesService // Key
    {
        public static partial class Key
        {
            private const string _UpdatesService =  nameof(UpdatesService) + ".";
            public const string OnUpdate = _UpdatesService + nameof(OnUpdate);
            public const string SubscribeUpdate = _UpdatesService + nameof(SubscribeUpdate);
            public static string OnUpdateFrame(int frame) => OnUpdate + "." + frame;

        }
    }
    public static partial class UpdatesService // Methods
    {
        public static void Subscribe(in (bool condition, int framecount, Action callback) data) => Key.SubscribeUpdate.Dispatch(data);
    }
}