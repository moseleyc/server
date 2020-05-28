﻿using Hybrasyl.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hybrasyl
{
    public class GuildMember
    {
        public Guid Rank { get; set; }
        public string Name { get; set; }
    }

    public class GuildRank
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class Guild
    {
        [JsonProperty]
        public Guid Identifier { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public List<GuildRank> Ranks { get; set; }
        public Board Board { get; set; }
        public GuildVault Vault { get; set; }
        [JsonProperty]
        public List<GuildMember> Members { get; set; }

        public string StorageKey => string.Concat(GetType(), ':', Name.ToLower());
        public bool IsSaving;


        public Guild(string name, User leader, List<User> founders)
        {
            Name = name;
            Identifier = new Guid();
            Ranks = new List<GuildRank>();

            //default ranks, with guid as key so naming can be changed by user
            Ranks.Add(new GuildRank() { Identifier = new Guid(), Name = "Guild Leader", Level = 0 });
            Ranks.Add(new GuildRank() { Identifier = new Guid(), Name = "Council", Level = 1 });
            Ranks.Add(new GuildRank() { Identifier = new Guid(), Name = "Founder", Level = 2 });
            Ranks.Add(new GuildRank() { Identifier = new Guid(), Name = "Member", Level = 3 });
            Ranks.Add(new GuildRank() { Identifier = new Guid(), Name = "Initiate", Level = 4 });
            GameLog.Info($"Guild {name}: Added default ranks");
            Board = new Board(name);
            GameLog.Info($"Guild {name}: Created guild board");
            Vault = new GuildVault();
            Members = new List<GuildMember>();

            var leaderGuid = Ranks.FirstOrDefault(x => x.Name == "Guild Leader").Identifier;
            var founderGuid = Ranks.FirstOrDefault(x => x.Name == "Founder").Identifier;

            Members.Add(new GuildMember() { Name = leader.Name, Rank = leaderGuid });
            GameLog.Info($"Guild {name}: Adding leader {leader.Name}");
            foreach (var founder in founders)
            {
                Members.Add(new GuildMember() { Name = founder.Name, Rank = founderGuid });
                GameLog.Info($"Guild {name}: Adding founder {founder.Name}");
            }


        }

        public void AddMember(string name)
        {
            var lowestRank = Ranks.Aggregate((r1, r2) => r1.Level > r2.Level ? r1 : r2);
            GameLog.Info($"Guild {Name}: Lowest guild rank identified as {lowestRank.Name}");
            Members.Add(new GuildMember() { Name = name, Rank = lowestRank.Identifier });
            GameLog.Info($"Guild {Name}: Adding new member {name} to rank {lowestRank.Name}");
        }

        public void RemoveMember(string name)
        {
            var member = Members.Single(x => x.Name == name);
            if(member.Rank == Ranks.Single(x => x.Level == 0).Identifier)
            {
                GameLog.Info($"Guild {Name}: Sorry, the guild leader can't be removed.");
                return;
            }
            Members.Remove(member);
            GameLog.Info($"Guild {Name}: Removing member {name}");
        }

        public void PromoteMember(string name)
        {
            var member = Members.Single(x => x.Name == name);
            var currentRank = Ranks.FirstOrDefault(x => x.Identifier == member.Rank);
            var newRank = Ranks.FirstOrDefault(x => x.Level == currentRank.Level - 1);

            if(newRank != null && newRank.Level > 0) //we can only have one leader
            {
                member.Rank = newRank.Identifier;
                GameLog.Info($"Guild {Name}: Promoting {member.Name} to rank {newRank.Name}");
            }
        }

        public void DemoteMember(string name)
        {
            var member = Members.Single(x => x.Name == name);
            var currentRank = Ranks.FirstOrDefault(x => x.Identifier == member.Rank);
            var newRank = Ranks.FirstOrDefault(x => x.Level == currentRank.Level + 1);

            if (newRank != null && newRank.Level > currentRank.Level)
            {
                if(currentRank.Level == 0)
                {
                    GameLog.Info($"Guild {Name}: Sorry, the guild leader cannot be demoted.");
                    return;
                }
                member.Rank = newRank.Identifier;
                GameLog.Info($"Guild {Name}: Demoting {member.Name} to rank {newRank.Name}");
            }
        }

        public void ChangeRankTitle(string oldTitle, string newTitle)
        {
            var rank = Ranks.FirstOrDefault(x => x.Name == oldTitle);

            if(rank != null)
            {
                rank.Name = newTitle;
                GameLog.Info($"Guild {Name}: Renaming rank {oldTitle} to rank {newTitle}");
            }
        }

        public void AddRank(string title) //adds a new rank at the lowest tier
        {
            if (Ranks.Any(x => x.Name == title)) return;

            var lowestRank = Ranks.Aggregate((r1, r2) => r1.Level > r2.Level ? r1 : r2);

            var rank = new GuildRank() { Identifier = new Guid(), Name = title, Level = lowestRank.Level + 1 };

            Ranks.Add(rank);
            GameLog.Info($"Guild {Name}: New rank {rank.Name} added as level {rank.Level}");
        }

        public void RemoveRank() //only remove the lowest tier rank and move all members in rank up one level.
        {
            var lowestRank = Ranks.Aggregate((r1, r2) => r1.Level > r2.Level ? r1 : r2);

            var nextRank = Ranks.FirstOrDefault(x => x.Level == lowestRank.Level - 1);

            if(nextRank != null && nextRank.Level != 0)
            {
                var moveMembers = Members.Where(x => x.Rank == lowestRank.Identifier).ToList();

                foreach(var member in moveMembers)
                {
                    member.Rank = nextRank.Identifier;
                    GameLog.Info($"Guild {Name}: Member {member.Name} moved to rank {nextRank.Name} due to rank deletion");
                }

                //remove lowest rank here to avoid missing members
                Ranks.Remove(lowestRank);
                GameLog.Info($"Guild {Name}: Deleted rank {lowestRank.Name}");
            }

        }


        public void Save()
        {
            if (IsSaving) return;
            IsSaving = true;
            var cache = World.DatastoreConnection.GetDatabase();
            cache.Set(StorageKey, this);
            IsSaving = false;
        }
    }
}
