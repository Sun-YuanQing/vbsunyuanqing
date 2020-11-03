delete [dbo].[t_gridview_menuL2]
delete [dbo].[t_gridview_menuL1]
delete [dbo].[t_gridview_menuL0]



select * from Students
select top 9  classid  =  case
 when classid='0'  then '请选择' 
 when  classid='1'  then '连跳一段' 
  when  sex='1'  then '男' 
 when classid='3'  then '连跳三段' 
 
  else '无效数据！'
 end 
 from Students


 select k.*,l2.* from(select * from t_gridview_menuL0 l0 
 left join t_gridview_menuL1 l1 on (l0.m0_id =l1.m1_m0id )) k 
  left join   t_gridview_menuL2 l2 on (l2 .m2_m1id =k .m1_id )
