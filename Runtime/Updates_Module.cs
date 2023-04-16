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
using System.Collections.Generic;
using UnityEngine;
using Kingdox.UniFlux;
namespace Kingdox.UniFlux.Updates
{
    public sealed partial class Updates_Module : MonoFlux
    {
        private readonly Dictionary<int, Action> dic_updates = new Dictionary<int, Action>();
        private int frameCount = 0;
        private void Update()
        {
            frameCount = Time.frameCount;
            if(dic_updates.TryGetValue(frameCount, out var callback))
            {
                callback.Invoke();
            }
            UpdatesService.Key.OnUpdate.Dispatch();
            UpdatesService.Key.OnUpdateFrame(frameCount).Dispatch();
        }
#if UNITY_EDITOR
        [SerializeField] private List<string> __debug_list_ = new List<string>();
        [SerializeField] private bool debug = true;
        private void OnGUI()
        {
            if (!Application.isPlaying) return;
            if (!debug) return;
            __debug_list_.Clear();
            frameCount = Time.frameCount;
            foreach (var item in dic_updates)
            {
                if (frameCount % item.Key == 0)
                {
                    __debug_list_.Add($"[{item.Key}] => {item.Value.GetInvocationList().Length} (INVOKE)");
                }
                else
                {
                    __debug_list_.Add($"[{item.Key}] => {item.Value.GetInvocationList().Length}");
                }
            }

        }
#endif
        [Flux(UpdatesService.Key.SubscribeUpdate)] private void SubscribeUpdate((bool condition,int framecount, Action callback) value)
        {
            if(dic_updates.TryGetValue(value.framecount, out var _actions))
            {
                if (value.condition) _actions += value.callback;
                else _actions -= value.callback;
            }
            else if (value.condition) dic_updates.Add(value.framecount, value.callback);
        }
    }
}
