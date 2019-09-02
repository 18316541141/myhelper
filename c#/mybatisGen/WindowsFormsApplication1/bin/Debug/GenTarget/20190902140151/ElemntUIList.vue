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
		                <el-input size="medium" v-model="postData.IROrderNoLike" placeholder="任务单号(办事易全送过来保证必须唯一)" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRWeiXinNickNameLike" placeholder="微信昵称" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRWeiXinHeaderImageLike" placeholder="微信头像图片相对路径" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRQrCodeImagePathLike" placeholder="二维码图片相对路径" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRHandleMessageLike" placeholder="处理情况信息，描述处理过程中的详细问题" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRReportPicPathJsonLike" placeholder="处理结果截图的json列表" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRRobotIdLike" placeholder="机器人编号" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRRemarkLike" placeholder="支付备注" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRScanPayNotifyRetLike" placeholder="办事易的ScanPayNotify接口返回结果" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRScanPayNotifyUrlLike" placeholder="该支付订单支付成功时回调。" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTaskIDStart" placeholder="自增id" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRHandleStateStart" placeholder="处理状态,0:未处理,1:处理完毕,2:处理失败" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRHandleTimeStart" placeholder="处理时间" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRCreateTimeStart" placeholder="任务登记时间" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTakeMoneyStart" placeholder="收款金额，单位：分" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRPushStateStart" placeholder="推送状态, 0:未推送, 1:推送成功, 2:推送失败" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRPushTimeStart" placeholder="推送时间" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTaskIDEnd" placeholder="自增id" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRHandleStateEnd" placeholder="处理状态,0:未处理,1:处理完毕,2:处理失败" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRHandleTimeEnd" placeholder="处理时间" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRCreateTimeEnd" placeholder="任务登记时间" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRTakeMoneyEnd" placeholder="收款金额，单位：分" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRPushStateEnd" placeholder="推送状态, 0:未推送, 1:推送成功, 2:推送失败" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRPushTimeEnd" placeholder="推送时间" clearable></el-input>
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
        <default-page ref="table" url="/api/IRobotQrCodePayTask/page" v-bind:post-data="postData" v-bind:ret-data.sync="retData" v-bind:reduce-height="120" v-bind:checked-datas.sync="checkedDatas"
            v-bind:show-checked="true" excel-title="测试数据.xlsx" export-url="/api/IRobotQrCodePayTask/export">

                    <el-table-column prop="IRTaskID" label="自增id" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IROrderNo" label="任务单号(办事易全送过来保证必须唯一)" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRWeiXinNickName" label="微信昵称" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRWeiXinHeaderImage" label="微信头像图片相对路径" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRQrCodeImagePath" label="二维码图片相对路径" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRHandleState" label="处理状态,0:未处理,1:处理完毕,2:处理失败" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRHandleMessage" label="处理情况信息，描述处理过程中的详细问题" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRHandleTime" label="处理时间" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRCreateTime" label="任务登记时间" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRReportPicPathJson" label="处理结果截图的json列表" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRTakeMoney" label="收款金额，单位：分" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRRobotId" label="机器人编号" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRRemark" label="支付备注" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRPushState" label="推送状态, 0:未推送, 1:推送成功, 2:推送失败" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRPushTime" label="推送时间" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRScanPayNotifyRet" label="办事易的ScanPayNotify接口返回结果" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRScanPayNotifyUrl" label="该支付订单支付成功时回调。" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
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
							
									<el-form-item label="自增id">
										<el-input v-model="formData.IRTaskID" placeholder="请填写自增id"></el-input>
									</el-form-item>			
									<el-form-item label="任务单号(办事易全送过来保证必须唯一)">
										<el-input v-model="formData.IROrderNo" placeholder="请填写任务单号(办事易全送过来保证必须唯一)"></el-input>
									</el-form-item>			
									<el-form-item label="微信昵称">
										<el-input v-model="formData.IRWeiXinNickName" placeholder="请填写微信昵称"></el-input>
									</el-form-item>			
									<el-form-item label="微信头像图片相对路径">
										<el-input v-model="formData.IRWeiXinHeaderImage" placeholder="请填写微信头像图片相对路径"></el-input>
									</el-form-item>			
									<el-form-item label="二维码图片相对路径">
										<el-input v-model="formData.IRQrCodeImagePath" placeholder="请填写二维码图片相对路径"></el-input>
									</el-form-item>			
									<el-form-item label="处理状态,0:未处理,1:处理完毕,2:处理失败">
										<el-input v-model="formData.IRHandleState" placeholder="请填写处理状态,0:未处理,1:处理完毕,2:处理失败"></el-input>
									</el-form-item>			
									<el-form-item label="处理情况信息，描述处理过程中的详细问题">
										<el-input v-model="formData.IRHandleMessage" placeholder="请填写处理情况信息，描述处理过程中的详细问题"></el-input>
									</el-form-item>			
									<el-form-item label="处理时间">
										<el-input v-model="formData.IRHandleTime" placeholder="请填写处理时间"></el-input>
									</el-form-item>			
									<el-form-item label="任务登记时间">
										<el-input v-model="formData.IRCreateTime" placeholder="请填写任务登记时间"></el-input>
									</el-form-item>			
									<el-form-item label="处理结果截图的json列表">
										<el-input v-model="formData.IRReportPicPathJson" placeholder="请填写处理结果截图的json列表"></el-input>
									</el-form-item>			
									<el-form-item label="收款金额，单位：分">
										<el-input v-model="formData.IRTakeMoney" placeholder="请填写收款金额，单位：分"></el-input>
									</el-form-item>			
									<el-form-item label="机器人编号">
										<el-input v-model="formData.IRRobotId" placeholder="请填写机器人编号"></el-input>
									</el-form-item>			
									<el-form-item label="支付备注">
										<el-input v-model="formData.IRRemark" placeholder="请填写支付备注"></el-input>
									</el-form-item>			
									<el-form-item label="推送状态, 0:未推送, 1:推送成功, 2:推送失败">
										<el-input v-model="formData.IRPushState" placeholder="请填写推送状态, 0:未推送, 1:推送成功, 2:推送失败"></el-input>
									</el-form-item>			
									<el-form-item label="推送时间">
										<el-input v-model="formData.IRPushTime" placeholder="请填写推送时间"></el-input>
									</el-form-item>			
									<el-form-item label="办事易的ScanPayNotify接口返回结果">
										<el-input v-model="formData.IRScanPayNotifyRet" placeholder="请填写办事易的ScanPayNotify接口返回结果"></el-input>
									</el-form-item>			
									<el-form-item label="该支付订单支付成功时回调。">
										<el-input v-model="formData.IRScanPayNotifyUrl" placeholder="请填写该支付订单支付成功时回调。"></el-input>
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
        
        导出excel
        
			exportExcel(command){
        this.