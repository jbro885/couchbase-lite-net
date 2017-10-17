﻿// 
// From.cs
// 
// Author:
//     Jim Borden  <jim.borden@couchbase.com>
// 
// Copyright (c) 2017 Couchbase, Inc All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using Couchbase.Lite.Query;

namespace Couchbase.Lite.Internal.Query
{
    internal sealed class From : LimitedQuery, IFrom
    {
        #region Constructors

        public From(XQuery query, IDataSource impl)
        {
            Copy(query);

            FromImpl = impl as QueryDataSource;
            Database = (impl as DatabaseSource)?.Database;
        }

        #endregion

        #region Public Methods

        public object ToJSON()
        {
            return FromImpl?.ToJSON();
        }

        #endregion

        #region IGroupByRouter

        public IGroupBy GroupBy(params IExpression[] expressions)
        {
            return new QueryGroupBy(this, expressions);
        }

        #endregion

        #region IJoinRouter

        public IJoin Joins(params IJoin[] joins)
        {
            return new QueryJoin(this, joins);
        }

        #endregion

        #region IOrderByRouter

        public IOrdering OrderBy(params IOrdering[] ordering)
        {
            return new QueryOrdering(this, ordering);
        }

        #endregion

        #region IWhereRouter

        public IWhere Where(IExpression expression)
        {
            return new Where(this, expression);
        }

        #endregion
    }
}