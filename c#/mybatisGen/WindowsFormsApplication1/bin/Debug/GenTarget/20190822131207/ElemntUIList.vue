<template>
    <div>
        <!-- 抄考代码
        标题部分：
        <el-breadcrumb separator-class="el-icon-arrow-right">
            <el-breadcrumb-item>一级菜单</el-breadcrumb-item>
            <el-breadcrumb-item>二级菜单</el-breadcrumb-item>
            <el-breadcrumb-item>三级菜单</el-breadcrumb-item>
            <el-breadcrumb-item>....</el-breadcrumb-item>
        </el-breadcrumb>
------------------------------------------------------------------------------------------------------
        查询参数表单：
		<br>
        <el-form v-bind:inline="true" v-bind:model="postData">
            <el-form-item>
              <el-date-picker size="medium" value-format="yyyy-MM-dd" type="date" placeholder="日期提取框" v-model="postData.日期字段"></el-date-picker>
            </el-form-item>
            <el-form-item>
              <el-select size="medium" v-model="postData.下拉框字段" placeholder="下拉框" clearable>
                <el-option :key="0" :label="'选项1'" :value="0"></el-option>
                <el-option :key="1" :label="'选项2'" :value="1"></el-option>
                <el-option :key="2" :label="'选项3'" :value="2"></el-option>
                ....
              </el-select>
            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IROrderNoLike" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRScanPayNotifyUrlLike" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRScanPayNotifyRetLike" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRQrCodeImagePathLike" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRReportPicPathLike" placeholder="" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTaskIDStart" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTakeMoneyStart" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRPushStateStart" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRHandleStateStart" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRBsyNotifyStateStart" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRSendMoneyNotifyStateStart" placeholder="" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTaskIDEnd" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTakeMoneyEnd" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRPushStateEnd" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRHandleStateEnd" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRBsyNotifyStateEnd" placeholder="" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRSendMoneyNotifyStateEnd" placeholder="" clearable></el-input>
		            </el-form-item>
            <el-form-item>
                <el-button size="medium" type="primary" v-on:click="$refs.table.search()" icon="el-icon-search">查询</el-button>
            </el-form-item>
            <el-form-item>
                <el-button size="medium" v-on:click="$refs.table.refresh()" icon="el-icon-refresh">刷新</el-button>
            </el-form-item>
            <el-form-item>
                <el-dropdown>
                    <el-button size="medium" type="success" icon="el-icon-download">导出excel</el-button>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-on:click="$refs.table.export('xlsx')">xlsx格式</el-dropdown-item>
                        <el-dropdown-item v-on:click="$refs.table.export('xls')">xls格式</el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
            </el-form-item>
			<el-form-item>
                <el-button size="medium" type="primary" v-on:click="add" icon="el-icon-plus">新增</el-button>
            </el-form-item>
			<transition name="fade">
                <el-form-item v-show="checkedDatas.length>0">
                    <el-button size="medium" type="primary" v-on:click="enable" icon="el-icon-check">启用</el-button>
                </el-form-item>
            </transition>
            <transition name="fade">
                <el-form-item v-show="checkedDatas.length>0">
                    <el-button size="medium" v-on:click="disable" icon="el-icon-close">禁用</el-button>
                </el-form-item>
            </transition>
            <transition name="fade">
                <el-form-item v-show="checkedDatas.length>0">
                    <el-button size="medium" type="danger" v-on:click="delBatch" icon="el-icon-delete">删除</el-button>
                </el-form-item>
            </transition>
        </el-form>
------------------------------------------------------------------------------------------------------
        列表部分：
        <default-page ref="table" url="/api/IRobotQrCodePayTask/page" v-bind:post-data="postData" v-bind:ret-data.sync="retData" v-bind:reduce-height="120" v-bind:checked-datas.sync="checkedDatas"
            v-bind:show-checked="true" excel-title="测试数据.xlsx" export-url="/api/IRobotQrCodePayTask/export">

                    <el-table-column prop="IRTaskID" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IROrderNo" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRTakeMoney" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRPushState" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRScanPayNotifyUrl" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRScanPayNotifyRet" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRHandleState" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRQrCodeImagePath" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRBsyNotifyState" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRSendMoneyNotifyState" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRReportPicPath" label="" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
			<el-table-column label="操作" fixed="right" width="170px">
              <template slot-scope="scope">
                <el-button size="small" type="primary" v-on:click="edit(scope.row.IRTaskID)">编辑</el-button>
                <el-button size="small" type="danger" v-on:click="del(scope.row.IRTaskID)">删除</el-button>
              </template>
            </el-table-column>
        </default-page>
------------------------------------------------------------------------------------------------------
		编辑部分：
		<el-dialog v-bind:title="(formData.IRTaskID===null?'新增':'编辑')+'***模块'" top="25px" width="300px"
			v-bind:visible.sync="editDialog"
			v-bind:modal-append-to-body="true"
			v-bind:append-to-body="true">
			<div style="width:100%;height:400px;overflow-y:auto;">
				<el-form ref="form" v-bind:model="formData" label-width="80px" v-on:keyup.enter.native="onSubmit">
							
									<el-form-item label="">
										<el-input v-model="formData.IRTaskID" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IROrderNo" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRTakeMoney" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRPushState" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRScanPayNotifyUrl" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRScanPayNotifyRet" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRHandleState" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRQrCodeImagePath" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRBsyNotifyState" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRSendMoneyNotifyState" placeholder="请填写"></el-input>
									</el-form-item>			
									<el-form-item label="">
										<el-input v-model="formData.IRReportPicPath" placeholder="请填写"></el-input>
									</el-form-item>
				  <el-form-item>
					  <el-button type="primary" v-on:click="onSubmit">保存</el-button>
					  <el-button v-on:click="editDialog=false;">取消</el-button>
				  </el-form-item>
				</el-form>
			</div>
		</el-dialog>
        -->
    </div>
</template>
<script>
export default {
    /* 抄考代码
        数据部分（和列表部分配合使用）：
        data(){
            return {
				formData:{},	//仅用于存放新增表单、编辑表单的数据
                postData:{},	//仅用于存放查询参数的数据
                retData:{},		//仅用于存放分页查询的返回结果
                checkedDatas:[],	//仅用于存放当前分页表格勾选项
				editDialog:false,	//编辑、新增表单的显示和隐藏
            };
        }
------------------------------------------------------------------------------------------------------
        methods:{
			
			  保存表单
			 
			onSubmit(){
				if(this.formData.IRTaskID===null){
					this.$post('/api/IRobotQrCodePayTask/add',this.formData,function(result){
						if(result.code===0){
							this.editDialog=false;
							this.$refs.table.refresh();
						}
					});
				}else{
					this.$post('/api/IRobotQrCodePayTask/edit',this.formData,function(result){
						if(result.code===0){
							this.editDialog=false;
							this.$refs.table.refresh();
						}
					});
				}
			},
			
			  新增一条数据
			 
			add(){
				for(var key in this.formData){
					if(this.formData.hasOwnProperty(key)){
						this.formData[key]=null;
					}
				}
				this.editDialog = true;
			},
			
			 编辑一条数据
			 
			edit(key){
				this.$get('/api/IRobotQrCodePayTask/load',{IRTaskID:key},function(result){
					this.formData=result.data;
					this.editDialog = true;
				});
			},
			
			  删除一条数据
			 
			del(key){
				this.$post('/api/IRobotQrCodePayTask/del',{ IRTaskID:key}, function(result){
					this.$refs.table.refresh();
				});
			},
			
			  点击启用触发
			 
			enable(){
				var postData={};
				var checkedDatas = this.checkedDatas;
				for(var i=0,len=checkedDatas.length;i<len;i++){
					postData['datas['+i+'].IRTaskID']=checkedDatas[i];
				}
				this.$post('/api/IRobotQrCodePayTask/changeStatus',postData,function(result){
					this.$refs.table.refresh();
				});
			},
			
			 点击禁用触发
			 
			disable(){
				var postData={};
				var checkedDatas = this.checkedDatas;
				for(var i=0,len=checkedDatas.length;i<len;i++){
					postData['datas['+i+'].IRTaskID']=checkedDatas[i];
				}
				this.$post('/api/IRobotQrCodePayTask/changeStatus',postData,function(result){
					this.$refs.table.refresh();
				});
			},
			
			点击删除用触发
			 
			delBatch(){
				var postData={};
				var checkedDatas = this.checkedDatas;
				for(var i=0,len=checkedDatas.length;i<len;i++){
					postData['datas['+i+'].IRTaskID']=checkedDatas[i];
				}
				this.$post('/api/IRobotQrCodePayTask/delBatch',postData,function(result){
					this.$refs.table.refresh();
				});
			}
        }
    */
}
</script>