﻿// (C) Copyright 2015 by Andrew Nicholas
//
// This file is part of SCaddins.
//
// SCaddins is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// SCaddins is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with SCaddins.  If not, see <http://www.gnu.org/licenses/>.

namespace SCaddins.SCulcase
{
    using System;

    [Flags]
    public enum ConversionTypes
    {
        None = 0,
        Text = 1,
        SheetNames = 2,
        ViewNames = 4,
        TitlesOnSheets = 8,
        RoomNames = 16
    }

    public enum ConversionMode
    {
        UpperCase,
        LowerCase,
        TitleCase
    }
}
