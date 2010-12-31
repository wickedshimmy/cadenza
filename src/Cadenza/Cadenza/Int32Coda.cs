//
// Int32.cs
//
// Author:
//   Jb Evain (jbevain@novell.com)
//   Jonathan Pryor <jpryor@novell.com>
//   Kim Johansson <hagbarddenstore@gmail.com>
//   David Siegel <djsiegel@gmail.com>
//
// Copyright (c) 2007-2008 Novell, Inc. (http://www.novell.com)
// Copyright (c) 2008 Kim Johansson
// Copyright (c) 2011 David Siegel
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;

namespace Cadenza {

	public static class Int32Coda {

		public static IEnumerable<int> Times (this int self)
		{
			if (self < 0)
				throw new ArgumentOutOfRangeException ("self", "must be >= 0");
			return CreateTimesIterator (self);
		}

		private static IEnumerable<int> CreateTimesIterator (int self)
		{
			for (int i = 0; i < self; i++) {
				yield return i;
			}
		}

		public static IEnumerable<int> UpTo (this int self, int limit)
		{
			for (int i = self; i <= limit; ++i)
				yield return i;
		}

		public static IEnumerable<int> DownTo (this int self, int limit)
		{
			for (int i = self; i >= limit; i--)
				yield return i;
		}

		public static IEnumerable<int> Step (this int self, int limit, int step)
		{
			for (int i = self; i <= limit; i += step)
				yield return i;
		}

		public static bool IsEven (this int value)
		{
			return (value & 0x1) == 0;
		}

		public static bool IsOdd (this int value)
		{
			return (value & 0x1) == 1;
		}
        
        public static TimeSpan Tick (this int value)
        {
            return TimeSpan.FromTicks (value);
        }
        
        public static TimeSpan Ticks (this int value)
        {
            return TimeSpan.FromTicks (value);
        }
        
        public static TimeSpan Millisecond (this int value)
        {
            return TimeSpan.FromMilliseconds (value);
        }
        
        public static TimeSpan Milliseconds (this int value)
        {
            return TimeSpan.FromMilliseconds (value);
        }
        
        public static TimeSpan Second (this int value)
        {
            return TimeSpan.FromSeconds (value);
        }
        
        public static TimeSpan Seconds (this int value)
        {
            return TimeSpan.FromSeconds (value);
        }
        
        public static TimeSpan Minute (this int value)
        {
            return TimeSpan.FromMinutes (value);
        }
        
        public static TimeSpan Minutes (this int value)
        {
            return TimeSpan.FromMinutes (value);
        }
        
        public static TimeSpan Hour (this int value)
        {
            return TimeSpan.FromHours (value);
        }
        
        public static TimeSpan Hours (this int value)
        {
            return TimeSpan.FromHours (value);
        }
        
        public static TimeSpan Day (this int value)
        {
            return TimeSpan.FromDays (value);
        }
        
        public static TimeSpan Days (this int value)
        {
            return TimeSpan.FromDays (value);
        }
	}
}
