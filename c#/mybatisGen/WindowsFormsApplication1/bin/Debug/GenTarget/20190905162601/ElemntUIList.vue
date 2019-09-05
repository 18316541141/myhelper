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
		                <el-input size="medium" v-model="postData.IEErrNoLike" placeholder="错误编号，错误信息的主键" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IEErrOrderNoLike" placeholder="发生错误的订单" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IEErrRobotIdLike" placeholder="发生错误的机器人ID" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IEErrPicLike" placeholder="错误截图路径的json字符串" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IEErrContextLike" placeholder="错误信息的具体内容" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IECreateDateStart" placeholder="错误记录创建日期" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IEHandleStatusStart" placeholder="处理状态：0 未处理、1 已处理" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IECreateDateEnd" placeholder="错误记录创建日期" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IEHandleStatusEnd" placeholder="处理状态：0 未处理、1 已处理" clearable></el-input>
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
        <default-page ref="table" url="/api/IRobotErrorMsg/page" v-bind:post-data="postData" v-bind:ret-data.sync="retData" v-bind:reduce-height="120" v-bind:checked-datas.sync="checkedDatas"
            v-bind:show-checked="true" excel-title="测试数据.xlsx" export-url="/api/IRobotErrorMsg/export">

                    <el-table-column prop="IEErrNo" label="错误编号，错误信息的主键" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IECreateDate" label="错误记录创建日期" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IEErrOrderNo" label="发生错误的订单" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IEErrRobotId" label="发生错误的机器人ID" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IEErrPic" label="错误截图路径的json字符串" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IEErrContext" label="错误信息的具体内容" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IEHandleStatus" label="处理状态：0 未处理、1 已处理" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
			<el-table-column label="操作" fixed="right" width="170px">
              <template slot-scope="scope">
                <el-button size="small" type="primary" v-on:click="edit(scope.row.IEErrNo)">编辑</el-button>
                <el-button size="small" type="danger" v-on:click="del(scope.row.IEErrNo)">删除</el-button>
              </template>
            </el-table-column>
        </default-page>
------------------------------------------------------------------------------------------------------
		编辑部分：
		<el-dialog v-bind:title="(formData.IEErrNo===null?'新增':'编辑')+'***模块'" top="25px" width="300px"
			v-bind:visible.sync="editDialog"
			v-bind:modal-append-to-body="true"
			v-bind:append-to-body="true">
			<div style="width:100%;height:400px;overflow-y:auto;">
				<el-form ref="form" v-bind:model="formData" label-width="80px" v-on:keyup.enter.native="onSubmit">
							
									<el-form-item label="错误编号，错误信息的主键">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IEErrNo" v-model="formData.IEErrNo" placeholder="请填写错误编号，错误信息的主键"></el-input>
									</el-form-item>			
									<el-form-item label="错误记录创建日期">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IECreateDate" v-model="formData.IECreateDate" placeholder="请填写错误记录创建日期"></el-input>
									</el-form-item>			
									<el-form-item label="发生错误的订单">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IEErrOrderNo" v-model="formData.IEErrOrderNo" placeholder="请填写发生错误的订单"></el-input>
									</el-form-item>			
									<el-form-item label="发生错误的机器人ID">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IEErrRobotId" v-model="formData.IEErrRobotId" placeholder="请填写发生错误的机器人ID"></el-input>
									</el-form-item>			
									<el-form-item label="错误截图路径的json字符串">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IEErrPic" v-model="formData.IEErrPic" placeholder="请填写错误截图路径的json字符串"></el-input>
									</el-form-item>			
									<el-form-item label="错误信息的具体内容">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IEErrContext" v-model="formData.IEErrContext" placeholder="请填写错误信息的具体内容"></el-input>
									</el-form-item>			
									<el-form-item label="处理状态：0 未处理、1 已处理">
										<el-input v-validate="'required|max:20'" data-vv-name="formData.IEHandleStatus" v-model="formData.IEHandleStatus" placeholder="请填写处理状态：0 未处理、1 已处理"></el-input>
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
			this.