//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataCache;
//using DataCache.Models;

//namespace TempUI
//{
//    public class IntegrationTwo
//    {
//        public void PrivilegesAndGroupsTest()
//        {
//            for (int privilegeIndex = 201; privilegeIndex < 206; privilegeIndex++)
//            {
//                if (!Cache.CacheData.t_privileges.Any(privilege => privilege.C_id == privilegeIndex))
//                {
//                    t_zl_privileges newPrivilege = t_zl_privileges.Createt_privileges(privilegeIndex);
//                    newPrivilege.privilege_name = privilegeIndex + ":privilege";
//                    Cache.CacheData.t_privileges.Add(newPrivilege);
//                }
//            }
//            Cache.CacheData.SaveChanges();

//            for (int groupIndex = 1; groupIndex < 11; groupIndex++)
//            {
//                if (!Cache.CacheData.t_privilege_groups.Any(group => group.C_id == groupIndex))
//                {
//                    t_privilege_groups newGroup = t_privilege_groups.Createt_privilege_groups(groupIndex);
//                    newGroup.group_name = "group:" + groupIndex;

//                    List<t_zl_privileges> allPrivileges = (from privilege in Cache.CacheData.t_privileges
//                                                        where privilege.C_id == 201 ||
//                                                              privilege.C_id == 202 ||
//                                                              privilege.C_id == 203 ||
//                                                              privilege.C_id == 204 ||
//                                                              privilege.C_id == 205
//                                                        select privilege).ToList();
//                    foreach (t_zl_privileges CurrPrivilege in allPrivileges)
//                    {
//                        newGroup.t_privileges.Add(CurrPrivilege);
//                    }
//                    Cache.CacheData.t_privilege_groups.Add(newGroup);
//                }
//            }
//            Cache.CacheData.SaveChanges();
//        }
//    }
//}
