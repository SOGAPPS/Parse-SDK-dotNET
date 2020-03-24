// Copyright (c) 2015-present, Parse, LLC.  All rights reserved.  This source code is licensed under the BSD-style license found in the LICENSE file in the root directory of this source tree.  An additional grant of patent rights can be found in the PATENTS file in the same directory.

using Parse.Abstractions.Management;
using Parse.Analytics.Internal;

namespace Parse.Analytics
{
    public interface IParseAnalyticsPlugins
    {
        void Reset();

        IParseCorePlugins CorePlugins { get; }
        IParseAnalyticsController AnalyticsController { get; }
    }
}