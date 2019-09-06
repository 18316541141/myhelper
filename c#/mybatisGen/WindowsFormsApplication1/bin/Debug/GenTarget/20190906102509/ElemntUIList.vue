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
		                <el-input size="medium" v-model="postData.IUUsernameLike" placeholder="用户名" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IUPasswordLike" placeholder="密码" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IUSignKeyLike" placeholder="签名的唯一标识" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IUSignSecretLike" placeholder="签名密钥" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IUCreateDateStart" placeholder="用户创建日期" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IUCreateDateEnd" placeholder="用户创建日期" clearable></el-input>
		            </el-form-item>
            <el-form-item>
                <el-button size="medium" type="primary" v-on:click="$refs.table.search()" icon="el-icon-search">查询</el-button>
            </el-form-item>
            <el-form-item>
                <el-button size="medium" v-on:click="$refs.table.refresh()" icon="el-icon-refresh">刷新</el-button>
            </el-form-item>
            <el-form-item>
                <el-dropdown v-on:command="exportExcel">
                    <el-button size="medium" type="success" icon="el-icon-download">导出excel</el-button>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item command="xlsx">xlsx格式</el-dropdown-item>
                        <el-dropdown-item command="xls">xls格式</el-dropdown-item>
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
        <default-page ref="table" url="/api/IRobotUser/page" v-bind:post-data="postData" v-bind:ret-data.sync="retData" v-bind:reduce-height="120" v-bind:checked-datas.sync="checkedDatas"
            v-bind:show-checked="true" excel-title="测试数据.xlsx" export-url="/api/IRobotUser/export">

                    <el-table-column prop="IUId" label="主键id" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
                    <el-table-column prop="IUUsername" label="用户名" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IUPassword" label="密码" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IUSignKey" label="签名的唯一标识" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IUSignSecret" label="签名密钥" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IUCreateDate" label="用户创建日期" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
			<el-table-column label="操作" fixed="right" width="170px">
              <template slot-scope="scope">
                <el-button size="small" type="primary" v-on:click="edit(scope.row.IUId)">编辑</el-button>
                <el-button size="small" type="danger" v-on:click="del(scope.row.IUId)">删除</el-button>
              </template>
            </el-table-column>
        </default-page>
------------------------------------------------------------------------------------------------------
		编辑部分：
		<el-dialog v-bind:title="(formData.IUId===null?'新增':'编辑')+'***模块'" top="25px" width="300px"
			v-bind:visible.sync="editDialog"
			v-bind:modal-append-to-body="true"
			v-bind:append-to-body="true">
			<div style="width:100%;height:400px;overflow-y:auto;">
				<el-form ref="form" v-bind:model="formData" label-width="80px" v-on:keyup.enter.native="onSubmit">
							
									<el-form-item label="主键id">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IUId" v-model="formData.IUId" placeholder="请填写主键id"></el-input>
									</el-form-item>			
									<el-form-item label="用户名">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IUUsername" v-model="formData.IUUsername" placeholder="请填写用户名"></el-input>
									</el-form-item>			
									<el-form-item label="密码">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IUPassword" v-model="formData.IUPassword" placeholder="请填写密码"></el-input>
									</el-form-item>			
									<el-form-item label="签名的唯一标识">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IUSignKey" v-model="formData.IUSignKey" placeholder="请填写签名的唯一标识"></el-input>
									</el-form-item>			
									<el-form-item label="签名密钥">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IUSignSecret" v-model="formData.IUSignSecret" placeholder="请填写签名密钥"></el-input>
									</el-form-item>			
									<el-form-item label="用户创建日期">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IUCreateDate" v-model="formData.IUCreateDate" placeholder="请填写用户创建日期"></el-input>
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
		mounted(){
			//校验
			this.$validator.localize('zh_CN',{
				custom:{
									'formData.IUId':{
										required:'主键id不能为空',
										max(fieldName,replaceArray){
											return '主键id最大长度不能大于'+replaceArray[0]+'个字符';
										},
										min(){
											
										}
									}					'formData.IUUsername':{
										required:'用户名不能为空',
										max(fieldName,replaceArray){
											return '用户名最大长度不能大于'+replaceArray[0]+'个字符';
										},
										min(){
											
										}
									}					'formData.IUPassword':{
										required:'密码不能为空',
										max(fieldName,replaceArray){
											return '密码最大长度不能大于'+replaceArray[0]+'个字符';
										},
										min(){
											
										}
									}					'formData.IUSignKey':{
										required:'签名的唯一标识不能为空',
										max(fieldName,replaceArray){
											return '签名的唯一标识最大长度不能大于'+replaceArray[0]+'个字符';
										},
										min(){
											
										}
									}					'formData.IUSignSecret':{
										required:'签名密钥不能为空',
										max(fieldName,replaceArray){
											return '签名密钥最大长度不能大于'+replaceArray[0]+'个字符';
										},
										min(){
											
										}
									}					'formData.IUCreateDate':{
										required:'用户创建日期不能为空',
										max(fieldName,replaceArray){
											return '用户创建日期最大长度不能大于'+replaceArray[0]+'个字符';
										},
										min(){
											
										}
									}
				}
			});
		},
------------------------------------------------------------------------------------------------------
        methods:{
        
        导出excel
        
			exportExcel(command){
				this.$refs.table.export(command);
			}
			  保存表单
			 
			onSubmit(){
				//校验
				this.$validateForm(()=>{
					if(this.formData.IUId===null){
						this.$post('/api/IRobotUser/add',this.formData,function(result){
							if(result.code===0){
								this.editDialog=false;
								this.$refs.table.refresh();
							}
						});
					}else{
						this.$post('/api/IRobotUser/edit',this.formData,function(result){
							if(result.code===0){
								this.editDialog=false;
								this.$refs.table.refresh();
							}
						});
					}
				});
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
				this.$get('/api/IRobotUser/load',{IUId:key},function(result){
					this.formData=result.data;
					this.editDialog = true;
				});
			},
			
			  删除一条数据
			 
			del(key){
				this.$post('/api/IRobotUser/del',{ IUId:key}, function(result){
					this.$refs.table.refresh();
				});
			},
			
			  点击启用触发
			 
			enable(){
				var postData={};
				var checkedDatas = this.checkedDatas;
				for(var i=0,len=checkedDatas.length;i<len;i++){
					postData['datas['+i+'].IUId']=checkedDatas[i];
				}
				this.$post('/api/IRobotUser/changeStatus',postData,function(result){
					this.$refs.table.refresh();
				});
			},
			
			 点击禁用触发
			 
			disable(){
				var postData={};
				var checkedDatas = this.checkedDatas;
				for(var i=0,len=checkedDatas.length;i<len;i++){
					postData['datas['+i+'].IUId']=checkedDatas[i];
				}
				this.$post('/api/IRobotUser/changeStatus',postData,function(result){
					this.$refs.table.refresh();
				});
			},
			
			点击删除用触发
			 
			delBatch(){
				var postData={};
				var checkedDatas = this.checkedDatas;
				for(var i=0,len=checkedDatas.length;i<len;i++){
					postData['datas['+i+'].IUId']=checkedDatas[i];
				}
				this.$post('/api/IRobotUser/delBatch',postData,function(result){
					this.$refs.table.refresh();
				});
			}
        }
    */
}
</script>