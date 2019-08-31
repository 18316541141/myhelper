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
		                <el-input size="medium" v-model="postData.IRMRobotIdLike" placeholder="机器人id（6位数）" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMIPLike" placeholder="机器人客户端ip" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMTeamViewIdLike" placeholder="该机器人的TeamView伙伴ID" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMTeamViewPasswordLike" placeholder="该机器人的TeamView密码" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMSunflowerIdLike" placeholder="该机器人的向日葵ID" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMSunflowerPasswordLike" placeholder="该机器人的向日葵ID" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMBankCardTailNumLike" placeholder="银行卡号最后四位" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMBankCardNameLike" placeholder="银行名称" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMPayPasswordLike" placeholder="支付密码" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMScanPayNotifyUrlLike" placeholder="支付成功时，通知办事易支付成功的url" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMWxUsernameLike" placeholder="机器人使用的微信账号" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMWxPasswordLike" placeholder="机器人使用的微信密码" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMRefreshBalanceLike" placeholder="刷新余额（0：不刷新）、（1：马上刷新）；该值设置为1时，机器人会尽可能马上刷新余额。" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMIDStart" placeholder="自增id" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMRobotStateStart" placeholder="机器人状态, 1:启用,0:停用" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMRuningTimeStart" placeholder="机器人当前运行日期" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMMaxPayTradeCountStart" placeholder="最大支付笔数" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMCurrentPayCountStart" placeholder="当前累计支付笔数" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMBalanceStart" placeholder="当前可支付余额" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMCreateTimeStart" placeholder="机器人信息登记时间" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMMinBalanceStart" placeholder="提示充值的最低余额" clearable></el-input>
		            </el-form-item>
		            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMIDEnd" placeholder="自增id" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMRobotStateEnd" placeholder="机器人状态, 1:启用,0:停用" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMRuningTimeEnd" placeholder="机器人当前运行日期" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMMaxPayTradeCountEnd" placeholder="最大支付笔数" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMCurrentPayCountEnd" placeholder="当前累计支付笔数" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMBalanceEnd" placeholder="当前可支付余额" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMCreateTimeEnd" placeholder="机器人信息登记时间" clearable></el-input>
		            </el-form-item>            <el-form-item>
		                <el-input size="medium" v-model="postData.IRMMinBalanceEnd" placeholder="提示充值的最低余额" clearable></el-input>
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
        <default-page ref="table" url="/api/IRobotRobotManager/page" v-bind:post-data="postData" v-bind:ret-data.sync="retData" v-bind:reduce-height="120" v-bind:checked-datas.sync="checkedDatas"
            v-bind:show-checked="true" excel-title="测试数据.xlsx" export-url="/api/IRobotRobotManager/export">

                    <el-table-column prop="IRMID" label="自增id" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRMRobotId" label="机器人id（6位数）" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMRobotState" label="机器人状态, 1:启用,0:停用" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRMRuningTime" label="机器人当前运行日期" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRMMaxPayTradeCount" label="最大支付笔数" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRMCurrentPayCount" label="当前累计支付笔数" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRMBalance" label="当前可支付余额" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRMIP" label="机器人客户端ip" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMCreateTime" label="机器人信息登记时间" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
                    <el-table-column prop="IRMTeamViewId" label="该机器人的TeamView伙伴ID" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMTeamViewPassword" label="该机器人的TeamView密码" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMSunflowerId" label="该机器人的向日葵ID" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMSunflowerPassword" label="该机器人的向日葵ID" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMBankCardTailNum" label="银行卡号最后四位" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMBankCardName" label="银行名称" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMMinBalance" label="提示充值的最低余额" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
              
              
                    <el-table-column prop="IRMPayPassword" label="支付密码" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMScanPayNotifyUrl" label="支付成功时，通知办事易支付成功的url" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMWxUsername" label="机器人使用的微信账号" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMWxPassword" label="机器人使用的微信密码" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
                    <el-table-column prop="IRMRefreshBalance" label="刷新余额（0：不刷新）、（1：马上刷新）；该值设置为1时，机器人会尽可能马上刷新余额。" sortable="custom" v-bind:show-overflow-tooltip="true" width="150px"></el-table-column>
              
              
			<el-table-column label="操作" fixed="right" width="170px">
              <template slot-scope="scope">
                <el-button size="small" type="primary" v-on:click="edit(scope.row.IRMID)">编辑</el-button>
                <el-button size="small" type="danger" v-on:click="del(scope.row.IRMID)">删除</el-button>
              </template>
            </el-table-column>
        </default-page>
------------------------------------------------------------------------------------------------------
		编辑部分：
		<el-dialog v-bind:title="(formData.IRMID===null?'新增':'编辑')+'***模块'" top="25px" width="300px"
			v-bind:visible.sync="editDialog"
			v-bind:modal-append-to-body="true"
			v-bind:append-to-body="true">
			<div style="width:100%;height:400px;overflow-y:auto;">
				<el-form ref="form" v-bind:model="formData" label-width="80px" v-on:keyup.enter.native="onSubmit">
							
									<el-form-item label="自增id">
										<el-input v-model="formData.IRMID" placeholder="请填写自增id"></el-input>
									</el-form-item>			
									<el-form-item label="机器人id（6位数）">
										<el-input v-model="formData.IRMRobotId" placeholder="请填写机器人id（6位数）"></el-input>
									</el-form-item>			
									<el-form-item label="机器人状态, 1:启用,0:停用">
										<el-input v-model="formData.IRMRobotState" placeholder="请填写机器人状态, 1:启用,0:停用"></el-input>
									</el-form-item>			
									<el-form-item label="机器人当前运行日期">
										<el-input v-model="formData.IRMRuningTime" placeholder="请填写机器人当前运行日期"></el-input>
									</el-form-item>			
									<el-form-item label="最大支付笔数">
										<el-input v-model="formData.IRMMaxPayTradeCount" placeholder="请填写最大支付笔数"></el-input>
									</el-form-item>			
									<el-form-item label="当前累计支付笔数">
										<el-input v-model="formData.IRMCurrentPayCount" placeholder="请填写当前累计支付笔数"></el-input>
									</el-form-item>			
									<el-form-item label="当前可支付余额">
										<el-input v-model="formData.IRMBalance" placeholder="请填写当前可支付余额"></el-input>
									</el-form-item>			
									<el-form-item label="机器人客户端ip">
										<el-input v-model="formData.IRMIP" placeholder="请填写机器人客户端ip"></el-input>
									</el-form-item>			
									<el-form-item label="机器人信息登记时间">
										<el-input v-model="formData.IRMCreateTime" placeholder="请填写机器人信息登记时间"></el-input>
									</el-form-item>			
									<el-form-item label="该机器人的TeamView伙伴ID">
										<el-input v-model="formData.IRMTeamViewId" placeholder="请填写该机器人的TeamView伙伴ID"></el-input>
									</el-form-item>			
									<el-form-item label="该机器人的TeamView密码">
										<el-input v-model="formData.IRMTeamViewPassword" placeholder="请填写该机器人的TeamView密码"></el-input>
									</el-form-item>			
									<el-form-item label="该机器人的向日葵ID">
										<el-input v-model="formData.IRMSunflowerId" placeholder="请填写该机器人的向日葵ID"></el-input>
									</el-form-item>			
									<el-form-item label="该机器人的向日葵ID">
										<el-input v-model="formData.IRMSunflowerPassword" placeholder="请填写该机器人的向日葵ID"></el-input>
									</el-form-item>			
									<el-form-item label="银行卡号最后四位">
										<el-input v-model="formData.IRMBankCardTailNum" placeholder="请填写银行卡号最后四位"></el-input>
									</el-form-item>			
									<el-form-item label="银行名称">
										<el-input v-model="formData.IRMBankCardName" placeholder="请填写银行名称"></el-input>
									</el-form-item>			
									<el-form-item label="提示充值的最低余额">
										<el-input v-model="formData.IRMMinBalance" placeholder="请填写提示充值的最低余额"></el-input>
									</el-form-item>			
									<el-form-item label="支付密码">
										<el-input v-model="formData.IRMPayPassword" placeholder="请填写支付密码"></el-input>
									</el-form-item>			
									<el-form-item label="支付成功时，通知办事易支付成功的url">
										<el-input v-model="formData.IRMScanPayNotifyUrl" placeholder="请填写支付成功时，通知办事易支付成功的url"></el-input>
									</el-form-item>			
									<el-form-item label="机器人使用的微信账号">
										<el-input v-model="formData.IRMWxUsername" placeholder="请填写机器人使用的微信账号"></el-input>
									</el-form-item>			
									<el-form-item label="机器人使用的微信密码">
										<el-input v-model="formData.IRMWxPassword" placeholder="请填写机器人使用的微信密码"></el-input>
									</el-form-item>			
									<el-form-item label="刷新余额（0：不刷新）、（1：马上刷新）；该值设置为1时，机器人会尽可能马上刷新余额。">
										<el-input v-model="formData.IRMRefreshBalance" placeholder="请填写刷新余额（0：不刷新）、（1：马上刷新）；该值设置为1时，机器人会尽可能马上刷新余额。"></el-input>
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