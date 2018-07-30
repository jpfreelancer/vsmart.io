using Nest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Extensions
{
    public static class ElasticsearchQueryBuilder
    {
        public static QueryContainer QueryBuilder(JObject json)
        {

            List<QueryContainer> rules = new List<QueryContainer>();
            foreach (JObject obj in json["rules"])
            {
                var field = obj["field"];
                if (field != null)
                {
                    var match_phrase_query = new MatchPhraseQuery
                    {
                        Field = field.ToString(),
                        Query = obj["value"].ToString(),
                        Slop = 10
                    };
                    rules.Add(match_phrase_query);
                }
                else
                {
                    rules.Add(QueryBuilder(obj));
                }
            }
            var condition = json["condition"].ToString();
            BoolQuery bool_query;
            if (condition == "AND")
            {
                bool_query = new BoolQuery
                {
                    Must = rules
                };
            }
            else
            {
                bool_query = new BoolQuery
                {
                    Should = rules,
                    MinimumShouldMatch = 1
                };
            }
            return new QueryContainer(bool_query);
        }
    }
}
