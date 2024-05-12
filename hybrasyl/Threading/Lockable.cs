﻿// This file is part of Project Hybrasyl.
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Affero General Public License as published by
// the Free Software Foundation, version 3.
// 
// This program is distributed in the hope that it will be useful, but
// without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
// for more details.
// 
// You should have received a copy of the Affero General Public License along
// with this program. If not, see <http://www.gnu.org/licenses/>.
// 
// (C) 2020-2023 ERISCO, LLC
// 
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

namespace Hybrasyl.Threading;

public class Lockable<T>
{
    private readonly object _lock = new();
    private T _value;

    public Lockable(T value)
    {
        Value = value;
    }

    public T Value
    {
        get
        {
            lock (_lock)
            {
                return _value;
            }
        }
        set
        {
            lock (_lock)
            {
                _value = value;
            }
        }
    }

    public static implicit operator T(Lockable<T> value) => value.Value;

    public static Lockable<T> operator -(Lockable<T> a, Lockable<T> b) => new(Difference(a.Value, b.Value));

    public static Lockable<T> operator +(Lockable<T> a, Lockable<T> b) => new(Sum(a.Value, b.Value));

    public static Lockable<T> operator *(Lockable<T> a, Lockable<T> b) => new(Product(a.Value, b.Value));

    private static T Sum(T a, T b) => (dynamic)a + (dynamic)b;

    private static T Difference(T a, T b) => (dynamic)a - (dynamic)b;

    private static T Product(T a, T b) => (dynamic)a * (dynamic)b;
}