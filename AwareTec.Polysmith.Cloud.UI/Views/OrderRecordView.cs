using AwareTec.Polysmith.Cloud.UI.Base;
using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.BuiltInType;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.FromDb;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.FromDbToDb;
using AwareTec.Polysmith.Cloud.UI.Forms;
using AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers.OrderRecordViewHelper;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs;
using AwareTec.Polysmith.Cloud.UI.Views.MiniModel;
using AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.Util.PathUtils;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    public partial class OrderRecordView : UserControl
    {
        #region 常量字符串
        private readonly string UPLOAD_AND_DOWNLOAD_PROGRESS_COLNAME = "UploadAndDownloadProgressCol";

        #region Exception
        private readonly string BIND_DATA_ERROR = "绑定的数据有误";
        private readonly string WRITE_ERROR = "修改路径记录时失败";

        #endregion

        #endregion

        #region 私有字段
        private int _rowIndex = -1;
        private OrderRecordPaginationViewModel _pagination = new OrderRecordPaginationViewModel();
        private GetMyOrderRequestModel _requestModel = new GetMyOrderRequestModel();
        private string _selectRowNo = "";
        #endregion

        #region 请求参数模型
        public GetMyOrderRequestModel RequestModel
        {
            set
            {
                _requestModel = value;
                _pagination.SelectedPageNo = 1;
                RefreshViewData();
            }
        }
        #endregion


        #region 事件属性
        private event EventHandler<SwitchPageEventArgs> _startMonitoringBeClicked;
        private event EventHandler<SwitchPageEventArgs> _enterPlayBeClicked;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("开始监测的快捷菜单被点击")]
        public event EventHandler<SwitchPageEventArgs> StartMonitoringBeClicked
        {
            add
            {
                if (value == null)
                    return;

                if (_startMonitoringBeClicked == null ||
                   _startMonitoringBeClicked.GetInvocationList().Length == 0)
                {
                    _startMonitoringBeClicked = value;
                }
                else
                {
                    foreach (var item in _startMonitoringBeClicked.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _startMonitoringBeClicked += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_startMonitoringBeClicked == null ||
                   _startMonitoringBeClicked.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _startMonitoringBeClicked.GetInvocationList())
                {
                    if (value == item)
                        _startMonitoringBeClicked -= value;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("进入回放的快捷菜单被点击")]
        public event EventHandler<SwitchPageEventArgs> EnterPlayBeClicked
        {
            add
            {
                if (value == null)
                    return;

                if (_enterPlayBeClicked == null ||
                   _enterPlayBeClicked.GetInvocationList().Length == 0)
                {
                    _enterPlayBeClicked = value;
                }
                else
                {
                    foreach (var item in _enterPlayBeClicked.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _enterPlayBeClicked += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_enterPlayBeClicked == null ||
                   _enterPlayBeClicked.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _enterPlayBeClicked.GetInvocationList())
                {
                    if (value == item)
                        _enterPlayBeClicked -= value;
                }
            }
        }
        #endregion

        #region 构造函数
        public OrderRecordView()
        {
            InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
            DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //OrderRecordPaginationView.OrderRecordPaginationViewModel = _pagination;
            _pagination.SelectedPageNoChanged += _pagination_SelectedPageNoChanged;
            this.Load += OrderRecordView_Load;
            this.MouseWheel += OrderRecordView_MouseWheel;
            this.Disposed += OrderRecordView_Disposed;
        }

        private void OrderRecordView_Load(object sender, EventArgs e)
        {
            m_KillTask = false;
            TaskRuning();
        }

        private void OrderRecordView_MouseWheel(object sender, MouseEventArgs e)
        {
            OrderRecordPaginationView.KeyPress(e.Delta > 0 ? Keys.Left : Keys.Right);
        }

        private void OrderRecordView_Disposed(object sender, EventArgs e)
        {
            m_KillTask = true;
            try
            {
                m_percentTh.Abort();
                m_percentTh.DisableComObjectEagerCleanup();
            }
            catch { }
        }

        #endregion

        #region 内置小模块方法

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="list"></param>
        /// <exception cref="NotSupportedException"></exception>
        private void RefreshData(List<OrderRecordViewModel> list)
        {
            try
            {
                foreach(TaskUion task in m_taskList)
                {
                    task.rowIndex = -1;
                }
                DataGridView.Invoke(new MethodInvoker(()=>
                {
                    DataGridView.Rows.Clear();      //清空表单数据

                    if (list == null || list.Count == 0)
                    {
                        foreach (var col in DataGridView.Columns)
                            (col as DataGridViewColumn).Visible = true;
                        return;
                    }
                    else
                    {
                        foreach (var col in DataGridView.Columns)
                            (col as DataGridViewColumn).Visible = false;
                    }

                    List<DataGridViewColumn> visibleCols = new List<DataGridViewColumn>();   //须隐藏的的列
                    List<OrderPath> addOrderPaths = new List<OrderPath>();                  //须在xml添加的订单路径
                    bool isAdd = false;
                    DataGridView.RowIndexOffSet =( _pagination.SelectedPageNo-1) * GetMaxCount();
                    for (int index = 0; index < list.Count; index++)
                    {
                        var item = list[index];
                        var propertyInfos = item.GetType().GetProperties();
                        int rowId = DataGridView.Rows.Add();
                        DataGridViewRow row = DataGridView.Rows[rowId];
                        row.Tag = item.OrderItem;
                        lock (m_taskLock)
                        {
                            var one = m_taskList.Find(t => t.OrderID == item.OrderItem.id);
                            if (one != null)
                            {
                                one.rowIndex = rowId;
                            }
                        }
                        if (item.OrderItem.no == _selectRowNo)
                            row.Selected = true;
                        foreach (var propertyInfo in propertyInfos)
                        {
                            var attr = CustomAttributeReader.Read(propertyInfo);

                            #region 判空检查(ViewModel属性无自定义特性则不显示)
                            if (attr == null)
                                continue;

                            if (string.IsNullOrWhiteSpace(attr.DataGridViewColumnName))
                                continue;

                            if (!DataGridView.Columns.Contains(attr.DataGridViewColumnName))
                                continue;
                            #endregion

                            #region 显示与隐藏列
                            var value = propertyInfo.GetValue(item);
                            if (attr.AlwaysVisible &&
                                value != null)
                            {
                                var findCol = visibleCols.Find(x => x.Name.Equals(attr.DataGridViewColumnName));
                                if (findCol == null)
                                    visibleCols.Add(DataGridView.Columns[attr.DataGridViewColumnName]);
                            }
                            #endregion

                            #region 设单元格的值
                            var propertyType = propertyInfo.PropertyType;
                            var cell = row.Cells[attr.DataGridViewColumnName];
                            switch (propertyType.Name)
                            {
                                case "String":
                                    cell.Value = propertyInfo.GetValue(item);
                                    break;
                                case "ImageAndStringCell":
                                    var imageAndString = propertyInfo.GetValue(item) as ImageAndStringCell;
                                    if (imageAndString != null)
                                    {
                                        cell.Value = string.IsNullOrWhiteSpace(imageAndString.DisplayName) ? string.Empty : imageAndString.DisplayName;
                                        var imageAndStringCell = cell as LinkAndImageCell;
                                        if (imageAndString.Image != null)
                                            imageAndStringCell.Image = imageAndString.Image;
                                    }
                                    break;
                                default:
                                    throw new NotSupportedException(GlobalReadonlyString.CommonException.EnumOutOfExpectedRange);
                            }
                            #endregion
                        }

                        #region 加载本地订单缓存数据, 新增缓存中未记录的订单数据
                        var orderPathData = GlobalSingleton.Instance.User.OrderPath;
                        var find = orderPathData.Find(x => (!string.IsNullOrWhiteSpace(x.OrderId)) &&
                                                            x.OrderId.Equals(item.OrderItem.id));
                        if (find == null)
                        {
                            addOrderPaths.Add(new OrderPath()
                            {
                                OrderId = item.OrderItem.id,
                                Version = 1
                            });
                            isAdd = true;
                        }
                        #endregion
                    }
                    if (isAdd)
                        GlobalSingleton.Instance.User.OrderPathXmlHelper.Modify(addOrderPaths);

                    visibleCols.ForEach(x => x.Visible = true);
                }));
            }
            catch(Exception ee)
            {
                throw ee;
            }
        }
        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        private List<OrderRecordViewModel> RequestData(GetMyOrderRequestModel requestModel)
        {
            requestModel.Mode = GlobalSingleton.Instance.User.ModeType;

            bool isSuccess = ApiRequest.Instance.GetMyOrder(requestModel, out ResponseModel responseModel);
            if (!isSuccess)
            {
                var failModel = responseModel as ResponseFailModel<GetMyOrderRequestModel>;
                MessageForm.Show(failModel.ToString());
                return null;
            }

            var model = (responseModel as ResponseSuccessModel<GetMyOrderResponseModel>).RestfulTable;
            _pagination.TotalRecordCount = model.totalCount;

            List<OrderRecordViewModel> viewModelList = new List<OrderRecordViewModel>();
            model.items.ToList().ForEach(x =>
            {
                var viewModel = new OrderRecordViewModel();
                viewModel.FromDb(x, null);
                viewModelList.Add(viewModel);
            });
            return viewModelList;
        }
        /// <summary>
        /// 获取本页能够容纳的最大行数据数量
        /// </summary>
        /// <returns></returns>
        private int GetMaxCount() => (this.DataGridView.Height - this.DataGridView.ColumnHeadersHeight) / this.DataGridView.RowTemplate.Height - 2;

        /// <summary>
        /// 复制Edf文件至云平台存储路径订单对应的路径中
        /// </summary>
        /// <param name="edfPath"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private string CopyEdfFile2OrderPath(string edfPath,
                                             TaskUion item)
        {
            if (string.IsNullOrWhiteSpace(edfPath) ||
                (!StringPath.PathExists(edfPath)))
                return null;

            string thisOrderPath = OrderPathHelper.GetAndGenerateOrderSavePath(item.OrderID);
            if (thisOrderPath == null)
                return null;

            string edfFileFullPath = thisOrderPath + "\\" + Path.GetFileName(edfPath);
            if (StringPath.PathExists(edfFileFullPath))
                File.Delete(edfFileFullPath);
                
            //复制Edf文件至云平台表单数据存储位置
            FileCopyHelper.Instance.CopyFileProgressIntChanged += Instance_CopyFileProgressIntChanged;
            FileCopyHelper.Instance.Copy(edfPath, edfFileFullPath, item);
            return edfFileFullPath;
        }

        /// <summary>
        /// 进入实时监测页面的通知
        /// </summary>
        private void EnterRealTime(object sender)
        {
            var data = GetSelectedRowData();

            var args = new SwitchPageEventArgs() { OrderItem = data };
            if (_startMonitoringBeClicked != null)
                _startMonitoringBeClicked(sender, args);
        }
        
        /// <summary>
        /// 获取当前选中行绑定的数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private OrderItem GetSelectedRowData()
        {
            OrderItem data = null;
            this.Invoke(new MethodInvoker(() =>
            {
                if (DataGridView == null)
                    throw new Exception("DataGridView为空");

                if (DataGridView.SelectedRows.Count == 0)
                    throw new Exception("当前未选中任一行数据");

                _rowIndex = DataGridView.SelectedRows[0].Index;
                data = DataGridView.SelectedRows[0].Tag as OrderItem;
            }));
            if (data == null)
                throw new Exception(BIND_DATA_ERROR);
  
            return data;    
        }
        #endregion

        #region  多线程执行任务
        private List<TaskUion> m_taskList = new List<TaskUion>();
        private bool m_KillTask = true;
        private System.Threading.Thread m_percentTh = null;
        private object m_taskLock = new object();

        private void TaskRuning()
        {
            m_percentTh = new System.Threading.Thread(() =>
            {
                bool change = false;
                while (!m_KillTask)
                {
                    int len = m_taskList.Count;
                    if (len > 0)
                    {
                        for (int i = 0; i < len; i++)
                        {
                            try
                            {
                                TaskUion one = m_taskList[i];
                                if (i == 0 && one.compelet == 0)
                                {
                                    one.ProgressEnable = true;
                                    if (one.upLoad)
                                    {
                                        one.pasue = false ;
                                        ////上传数据
                                        UpLoadFile(one);
                                    }
                                    else
                                    {
                                        one.pasue = false;
                                        ///下载数据
                                        LoadDownFile(one);
                                    }
                                    one.compelet = 1;
                                    change = true;
                                }
                                if (one.rowIndex >= 0 && !one.cancel && one.ProgressEnable)
                                {
                                    if (one.compelet > 0)
                                    {
                                        if (!one.pasue)
                                        {
                                            if (one.showValue != DataGridView.Rows[one.rowIndex].Cells[UPLOAD_AND_DOWNLOAD_PROGRESS_COLNAME].Value.ToString())
                                                DataGridView.Rows[one.rowIndex].Cells[UPLOAD_AND_DOWNLOAD_PROGRESS_COLNAME].Value = one.showValue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (one.cancel)
                                    {
                                        DataGridView.Rows[one.rowIndex].Cells[UPLOAD_AND_DOWNLOAD_PROGRESS_COLNAME].Value = "";
                                    }
                                    else
                                    {
                                        if (!DataGridView.Rows[one.rowIndex].Cells[UPLOAD_AND_DOWNLOAD_PROGRESS_COLNAME].Value.ToString().Contains("...") || change)
                                        {
                                            DataGridView.Rows[one.rowIndex].Cells[UPLOAD_AND_DOWNLOAD_PROGRESS_COLNAME].Value = string.Format("{0}(等待{1}...)", one.upLoad ? "上传" : "下载", i);
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                        change = false;
                        lock (m_taskLock)
                            m_taskList.RemoveAll(t => t.compelet == 2 || t.cancel);
                        System.Threading.Thread.Sleep(200);
                    }
                    else
                        System.Threading.Thread.Sleep(1000);
                }
            });
            m_percentTh.IsBackground = true;
            m_percentTh.SetApartmentState(System.Threading.ApartmentState.MTA);
            m_percentTh.Start();
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="task"></param>
        private void UpLoadFile(TaskUion task)
        {
            Task.Factory.StartNew(() =>
            {
                if (task.LocalZipPath == "")
                {
                    string edfFullPath = CopyEdfFile2OrderPath(task.sourceFilePath, task);
                    if (edfFullPath == null)
                    {
                        MessageForm.Show("复制Edf文件失败");
                    }
                }
                else
                {
                    ZipFileHelper.Instance.ZipFileProgressIntChanged += ZipFileProgressIntChanged;
                    ZipFileHelper.Instance.ZipFileWithProgress(task.sourceFilePath, task.LocalZipPath, task);
                }
            });
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="task"></param>
        private void LoadDownFile(TaskUion task)
        {
            Task.Factory.StartNew(() =>
            {
                string edfZipPath = string.Empty;
                try
                {
                    OrderItem data = (OrderItem)task.Tag;
                    if (data == null)
                        throw new Exception("参数传入错误");
                    string currentCloudOrderPath = OrderPathHelper.GetAndGenerateOrderSavePath(data);
                    if (currentCloudOrderPath == null)
                        throw new Exception("未找到云平台菲诗奥存储路径");

                    var paramsModel = new DownloadEdfParamsModel()
                    {
                        dir = currentCloudOrderPath,
                        edfFileName = task.OrderID,
                        order = data
                    };
                    if (paramsModel == null) throw new Exception("进度传参数据类型有误");
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("正在获取和生成Edf所在目录", pSystem.LogManagement.LogLevel.DEBUG);

                    string zipPath = string.Format("{0}\\{1}.zip", paramsModel.dir, paramsModel.edfFileName);
                    task.LocalZipPath = zipPath;
                    task.sourceFilePath = paramsModel.dir;

                    DownloadFileHelper.DownFileProgressIntChanged += DownFileProgressIntChanged;

                    edfZipPath = DownloadFileHelper.Download(paramsModel.dir,
                                                             paramsModel.edfFileName,
                                                             paramsModel.order.id,
                                                             FileType.Edf,
                                                             task);
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("从远端下载Edf文件完成", pSystem.LogManagement.LogLevel.DEBUG);

                    TaskUion zipuion = m_taskList.Find(t => t.OrderID == data.id);
                    if (zipuion != null)
                    {
                        SharpZipLibHelper.UnZipFile(edfZipPath,
                                                    paramsModel.dir,
                                                    false);
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("解压文件成功", pSystem.LogManagement.LogLevel.DEBUG);
                        zipuion.compelet = 2;
                        zipuion.showValue = "解压成功";
                    }
                    else
                    {
                        AhDung.MessageTip.ShowOk("解压失败");
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("解压文件失败", pSystem.LogManagement.LogLevel.ERROR);
                    }
                    if (!OrderPathHelper.WriteNewOrderPath(paramsModel.order, paramsModel.dir))
                        AhDung.MessageTip.ShowError(WRITE_ERROR);
                    else
                        AhDung.MessageTip.ShowOk("Edf已下载完成");
                }
                catch (Exception ex)
                {
                    AhDung.MessageTip.ShowError("下载文件过程中出错");
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("下载文件过程中出错，错误信息为 {0}", ex.Message), pSystem.LogManagement.LogLevel.ERROR);
                }
                finally
                {
                    DownloadFileHelper.HasDownDataIndex = 0;
                    if ((!string.IsNullOrWhiteSpace(edfZipPath)) &&
                        StringPath.PathExists(edfZipPath))
                    {
                        File.Delete(edfZipPath);
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("删除压缩包文件成功", pSystem.LogManagement.LogLevel.DEBUG);
                    }
                }
            });
        }
        
        /// <summary>
        /// 检查 上传或者下载任务单元 是否正在工作
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        private bool CheckTaskUionIsRunning(string orderid)
        {
            foreach (TaskUion taskUion in m_taskList)
            {
                if (taskUion.OrderID == orderid)
                {
                    AhDung.MessageTip.ShowWarning("该文件正在下载中，请勿重复下载");
                    return false;
                }
                if (!taskUion.upLoad && taskUion.compelet != 2)
                {
                    AhDung.MessageTip.ShowWarning("其他文件正在下载中，请耐心等待");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 事件绑定
        #region 整个UserControl的事件绑定
        private void DataGridView_SizeChanged(object sender, EventArgs e)
        {
            if (GetMaxCount() <= 0) return;
            _pagination.RecordCountPerPage = GetMaxCount();
            RefreshViewData();
        }
        #endregion

        #region 分页视图的事件绑定
        private void _pagination_SelectedPageNoChanged(object sender, DataModels.ViewModel.CustomEventArgs.SelectedPageNoChangedEventArgs e)
        {
            _pagination.SelectedPageNo = e.SelectedPageNo;
            RefreshViewData();
        }
        #endregion

        #region DataGridView的事件绑定
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var data = (DataGridView.Rows[e.RowIndex].Tag as OrderItem);
            if (m_taskList.Find(t => t.OrderID == data.id) != null)
            {
                return;
            }
            var function = GetFunctionTypeHelper.Get(data.status);
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户 双击首页的记录数据,记录订单号为 {0},类别为 {1},病例号为 {2},病人姓名为 {3}",
                          data.no, data.examType == ExamType.PrimaryScreeningTest ? "初筛" : "多导", data.medicalNo,
                          data.patientName), pSystem.LogManagement.LogLevel.INFO);
            switch (function)
            {
                case EnumFunction.开始监测:
                    StartMonitoring.PerformClick();
                    break;
                case EnumFunction.继续监听:
                    ContinueMonitoring.PerformClick();
                    break;
                case EnumFunction.上传edf:
                    UploadEdf.PerformClick();
                    break;
                case EnumFunction.进入回放:
                    EnterPlayBack.PerformClick();
                    break;
                case EnumFunction.查看数据:
                    ShowData.PerformClick();
                    break;
                case EnumFunction.下载edf:
                    DownloadEdf.PerformClick();
                    break;
                default:
                    break ;
            }
        }

        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0)
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

            //设置DataGridView的选中行
            foreach (var item in DataGridView.Rows)
                (item as DataGridViewRow).Selected = false;
            DataGridView.Rows[e.RowIndex].Selected = true;
            var data = GetSelectedRowData();
            _selectRowNo = data.no;

            switch (e.Button)
            {
                case MouseButtons.Right:
                    DataGridView_CellMouseRightClick();
                    break;
                case MouseButtons.Left:
                    DataGridView_CellMouseLeftClick(e.ColumnIndex);
                    break;
            }                
        }

        private void DataGridView_CellMouseRightClick()
        {
            var data = GetSelectedRowData();
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户 右键点击首页的记录数据,记录订单号为 {0},类别为 {1},病例号为 {2},病人姓名为 {3}",
              data.no, data.examType == ExamType.PrimaryScreeningTest ? "初筛" : "多导", data.medicalNo,
              data.patientName), pSystem.LogManagement.LogLevel.INFO);

            if (m_taskList.Find(t => t.OrderID == data.id) != null)
                return;
            #region 根据订单状态调整右键快捷菜单的可见性
            //全部隐藏
            foreach (var item in RightClickMenuStrip.Items)
                (item as ToolStripMenuItem).Visible = false;
            //将需要可见的设为可见
            var funs = GetFunctionTypeHelper.GetVisibleFunctions(data.status);
            if (funs == null || funs.Length == 0)
                return;

            foreach (var fun in funs)
            {
                switch (fun)
                {
                    case EnumFunction.开始监测:
                        StartMonitoring.Visible = true;
                        break;
                    case EnumFunction.继续监听:
                        ContinueMonitoring.Visible = true;
                        break;
                    case EnumFunction.进入回放:
                        EnterPlayBack.Visible = true;
                        break;
                    case EnumFunction.查看数据:
                        ShowData.Visible = true;
                        break;
                    case EnumFunction.下载edf:
                        DownloadEdf.Visible = true;
                        break;
                    case EnumFunction.上传edf:
                        UploadEdf.Visible = true;
                        break;
                    case EnumFunction.None:
                        break;
                    default:
                        return;
                }
            }
            #endregion
            RightClickMenuStrip.Show(Cursor.Position);
        }

        private void DataGridView_CellMouseLeftClick(int columnIndex)
        {
            var data = GetSelectedRowData();
            
            switch (DataGridView.Columns[columnIndex].Name)
            {
                case "OrderNumberCol":
                    Clipboard.SetDataObject(data.no, true);
                    break;
                case "AtlasCol":
                    ShowAtlas();
                    break;
                case "ReportCol":
                    ShowReport();
                    break;
            }
        }
        #endregion

        #region 快捷菜单按钮点击事件绑定

        /// <summary>
        /// 开始监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMonitoring_Click(object sender, EventArgs e)
        {
            var data = GetSelectedRowData();
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("执行 开始监测 功能，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));
            EnterRealTime(sender);
        }

        /// <summary>
        /// 继续监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueMonitoring_Click(object sender, EventArgs e)
        {
            var data = GetSelectedRowData();
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("执行 继续监听 功能，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));
            EnterRealTime(sender);
        }

        /// <summary>
        /// 进入回放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void EnterPlayBack_Click(object sender, EventArgs e)
        {
            EDF.Default.Interrupt = false;
            var data = GetSelectedRowData();
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("执行 进入回放 功能，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));

            if (!OrderPathHelper.LookForEdfAsMuchAsPossible(data, out string edfPath))
            {
                if (!CheckTaskUionIsRunning(data.id))
                    return;
                if (MessageForm.Show("Edf文件缺失, 是否下载？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    return;
                else
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("Edf文件缺失",pSystem.LogManagement.LogLevel.DEBUG);
                    DownloadEdf.PerformClick();
                }
            }

            if (!OrderPathHelper.LookForEdfAsMuchAsPossible(data, out string edfAgainPath))
                return;
            var args = new SwitchPageEventArgs()
            {
                OrderItem = data,
                EdfPath = string.IsNullOrWhiteSpace(edfPath) ? edfAgainPath : edfPath,
            };
            if (_enterPlayBeClicked != null)
                _enterPlayBeClicked(sender, args);
        }

        /// <summary>
        /// 上传Edf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadEdf_Click(object sender, EventArgs e)
        {
            EDF.Default.Interrupt = false;
            string edfFullPath = null;
            try
            {
                OrderItem data = GetSelectedRowData();
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("执行 上传edf 功能，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));
                if (m_taskList.Find(t => t.OrderID == data.id) == null)
                {
                    TaskUion task = new TaskUion()
                    {
                        OrderID = data.id,
                        compelet = 0,
                        upLoad = true,
                        rowIndex = _rowIndex,
                        showValue = "",
                        Tag = data
                    };
                    if (!OrderPathHelper.LookForEdfAsMuchAsPossible(data, out string edfPath))
                    {
                        var dialog = new AutoSearchEDFDialog();
                        dialog.Init(data.id);
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {

                            string selectedEdfPath = dialog.EdfPath;
                            task.sourceFilePath = selectedEdfPath;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        edfFullPath = edfPath;
                        string zipPath = Path.GetDirectoryName(edfFullPath) + "\\" + data.id + ".zip";
                        if (StringPath.PathExists(zipPath))
                            File.Delete(zipPath);
                        task.sourceFilePath = edfFullPath;
                        task.LocalZipPath = zipPath;
                    }
                    lock (m_taskLock)
                        m_taskList.Add(task);
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("任务正在执行中...");
                }
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message);
            }
        }

        /// <summary>
        /// 查看数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowData_Click(object sender, EventArgs e)
        {
            var data = GetSelectedRowData();
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("执行 查看数据 功能，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));
            if (!OrderPathHelper.LookForEdfAsMuchAsPossible(data, out string edfPath))
            {
                MessageForm.Show("无数据, 请下载");
                return;
            }
            if (File.Exists(edfPath))
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = false;
                //设置运行文件
                startInfo.FileName = "explorer";
                startInfo.Arguments = string.Format("/select,{0}", edfPath);
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                //如果不是管理员，则启动UAC
                System.Diagnostics.Process.Start(startInfo);
            }
            else
            {
                MessageForm.Show("无数据, 请下载");
                return;
            }
        }

        /// <summary>
        /// 下载Edf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadEdf_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选中行绑定的数据
                var data = GetSelectedRowData();
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("执行 下载edf 功能，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));

                if (!CheckTaskUionIsRunning(data.id))
                    return;

                if (OrderPathHelper.LookForEdfAsMuchAsPossible(data, out string edfPath))
                {
                    MessageForm.Show("已存在本地Edf文件, 请勿重复下载");
                    return;
                }
                string currentCloudOrderPath = OrderPathHelper.GetAndGenerateOrderSavePath(data);
                if (currentCloudOrderPath == null)
                    throw new Exception("未找到云平台菲诗奥存储路径");

                if (m_taskList.Find(t => t.OrderID == data.id) == null)
                {
                    TaskUion task = new TaskUion()
                    {
                        OrderID = data.id,
                        compelet = 0,
                        upLoad = false,
                        rowIndex = _rowIndex,
                        showValue = "",
                        Tag = data
                    };
                    lock (m_taskLock)
                        m_taskList.Add(task);
                    DownloadFileHelper.Interrupt = false;
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("任务正在执行中...");
                }
            }
            catch (Exception ee)
            {
                MessageForm.Show(ee.Message);
            }
        }

        #endregion

        #region 单元格功能点击处理
        /// <summary>
        /// 查看图谱
        /// </summary>
        private void ShowAtlas()
        {
            try
            {
                var data = GetSelectedRowData();
                if (data.status != OrderStatus.ToBeAudit &&
                   data.status != OrderStatus.Completed)
                {
                    AhDung.MessageTip.ShowWarning("只有待审核和已完成的订单才能查看图谱");
                    return;
                }
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户 点击首页记录表中图谱一列的查看按钮，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));

                var moudle = new Moudle();
                moudle.ControlBox = true;
                moudle.MaximizeBox = true;
                moudle.CanResize = true;
                moudle.StartPosition = FormStartPosition.CenterParent;
                moudle.Text = "图谱预览";
                moudle.Size = new System.Drawing.Size(900, 1000);
                moudle.KeyPreview = true;
                var mulReportChart = new MulReportChart();
                mulReportChart.Dock = DockStyle.Fill;
                mulReportChart.CurrentFrameNo = 1;
                mulReportChart.LoadData(ResultDomain.Default, Channel.Default.IsBreathOnly);
                moudle.Controls.Add(mulReportChart);
                int num66 = (int)moudle.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message);
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("查看图谱出错，错误信息为 {0}",ex.Message),pSystem.LogManagement.LogLevel.ERROR);
            }
        }

        /// <summary>
        /// 查看报告
        /// </summary>
        private void ShowReport()
        {
            string reportPath = string.Empty;
            try
            {
                OrderItem data = GetSelectedRowData();
                if (data.status != OrderStatus.Completed)
                {
                    AhDung.MessageTip.ShowWarning("只有已完成的订单才能查看报告");
                    return;
                }
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户 点击首页记录表中报告一列的查看按钮，记录订单号为 {0},病人姓名为 {1}", data.no, data.patientName));
                if (!CheckTaskUionIsRunning(data.id))
                    return;
                var fullUserConfig = GlobalSingleton.Instance.FullUserConfig;
                DirectoryInfo dir = new DirectoryInfo(fullUserConfig.ReportPath);
                var file = dir.GetFiles().ToList().Find(x => x.Name.Split('.').First().Equals(data.id));
                if (file != null)
                    file.Delete();
                string downFileName = string.Format("{1}({2}){4}报告{0}--{3}", Convert.ToDateTime(data.ActualExamTime).ToString("yyyyMMddHHmmss"), data.patientName, data.medicalNo, data.doctor == null ? "管理员" : data.doctor, data.examType == ExamType.PrimaryScreeningTest ? "初筛" : "睡眠分析");
                reportPath = DownloadFileHelper.Download(fullUserConfig.ReportPath,
                                                            downFileName, 
                                                            data.id,
                                                            FileType.Report);
                ShowFileByExtensionHelper.Show(reportPath);
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message);
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("查看报告出错，错误信息为 {0}", ex.Message), pSystem.LogManagement.LogLevel.ERROR);
            }
            finally
            {
                //之前进行了删除有关操作 ，这里不需要额外删除
                //if (StringPath.PathExists(reportPath))
                //    File.Delete(reportPath);
            }
        }
        #endregion
        #endregion

        #region 进度相关
        private void DownLoadEdf_CanceledHandle()
        {
            DownloadFileHelper.Interrupt = true;
        }

        private void DownLoadEdf_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            string edfZipPath = string.Empty;
            try
            {
                var paramsModel = e.Argument as DownloadEdfParamsModel;
                if (paramsModel == null) throw new Exception("进度传参数据类型有误");
                sender.SetProgress(10, "正在获取和生成Edf所在目录");

                edfZipPath = DownloadFileHelper.Download(paramsModel.dir,
                                                         paramsModel.edfFileName,
                                                         paramsModel.order.id,
                                                         FileType.Edf);
                if (sender.CancellationPending)
                {
                    DownloadFileHelper.Interrupt = true;
                    e.Cancel = true;
                }
                else
                {
                    sender.SetProgress(60, "从远端下载Edf文件完成");
                    SharpZipLibHelper.UnZipFile(edfZipPath,
                                                paramsModel.dir,
                                                false);
                    if (sender.CancellationPending)
                    {
                        DownloadFileHelper.Interrupt = true;
                        e.Cancel = true;
                    }
                    else
                    {
                        sender.SetProgress(90, "解压文件成功");
                        if (!OrderPathHelper.WriteNewOrderPath(paramsModel.order,
                                                              paramsModel.dir))
                            throw new Exception(WRITE_ERROR);
                        if (sender.CancellationPending)
                        {
                            DownloadFileHelper.Interrupt = true;
                            e.Cancel = true;
                        }
                        else
                        {
                            sender.SetProgress(100, "下载成功");
                            AhDung.MessageTip.ShowOk("Edf已下载完成");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sender.SetError(ex.Message);
            }
            finally
            {
                if ((!string.IsNullOrWhiteSpace(edfZipPath)) &&
                    StringPath.PathExists(edfZipPath))
                {
                    File.Delete(edfZipPath);
                    if (sender.CancellationPending)
                    {
                        DownloadFileHelper.Interrupt = true;
                        e.Cancel = true;
                    }
                    else
                    {
                        sender.SetProgress(100, "文件下载成功");
                    }
                }
            }
        }

        /// <summary>
        /// 断点续传上传Edf 进度显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_FileSliceUploadResult(object sender, FileSliceUploadResultEventArgs e)
        {
            if (e.IsSuccessful &&
               e.TotalPieces == e.CurrentPieces)
                FileResumableUploadsHelper.Instance.FileSliceUploadResult -= Instance_FileSliceUploadResult;

            if (e.IsSuccessful)
            {
                string displayTipText = string.Empty;
                if (e.CurrentPieces < e.TotalPieces)
                {
                    displayTipText = string.Format("上传:{0:f2}%", e.CurrentPieces * 100 / e.TotalPieces);
                    e.Order.showValue = displayTipText;
                }
                else
                {
                    displayTipText = "上传成功";
                    e.Order.showValue = displayTipText;

                    //更新本地订单记录数据
                    if (e.Order != null &&
                       (!string.IsNullOrWhiteSpace(e.EdfPath)) &&
                       StringPath.PathExists(e.EdfPath))
                    {
                        if (!OrderPathHelper.WriteNewOrderPath(e.Order.Tag as OrderItem, e.EdfPath))
                            MessageForm.Show(WRITE_ERROR);
                    }
                    e.Order.compelet = 2;
                    RefreshViewData();
                    if (StringPath.PathExists(e.Order.LocalZipPath))
                        File.Delete(e.Order.LocalZipPath);
                    AhDung.MessageTip.ShowOk("上传Edf成功");
                }
            }
            else
            {
                FileResumableUploadsHelper.Instance.FileSliceUploadResult -= Instance_FileSliceUploadResult;
                MessageForm.Show(e.ErrorMessage);
            }
        }

        /// <summary>
        /// 上传压缩文件进度显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZipFileProgressIntChanged(object sender, ZipFileEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.ErrorMessage))
            {
                string displayTipText = string.Empty;
                if (!e.IsSuccessful)
                {
                    displayTipText = string.Format("压缩:{0:f2}%", e.Percentage * 100);
                    e.Order.showValue = displayTipText;
                }
                else
                {
                    ZipFileHelper.Instance.ZipFileProgressIntChanged -= ZipFileProgressIntChanged;
                    displayTipText = "压缩成功";
                    e.Order.showValue = displayTipText;
                    FileResumableUploadsHelper.Instance.FileSliceUploadResult += Instance_FileSliceUploadResult;
                    FileResumableUploadsHelper.Instance.UploadFile(e.ZipPath, e.EdfPath, e.Order);
                }
            }
            else
            {
                ZipFileHelper.Instance.ZipFileProgressIntChanged -= ZipFileProgressIntChanged;
                if ((!string.IsNullOrWhiteSpace(e.ZipPath)) &&
                    StringPath.PathExists(e.ZipPath))
                    File.Delete(e.ZipPath);

                MessageForm.Show("压缩过程中出现错误," + e.ErrorMessage);
            }   
        }

        /// <summary>
        /// 下载文件进度显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownFileProgressIntChanged(object sender, FileSliceDownloadResultEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.ErrorMessage))
            {
                string displayTipText = string.Empty;
                if (!e.IsSuccessful)
                {
                    displayTipText = string.Format("下载:{0:f2}%", e.Percentage * 100);
                    e.Order.showValue = displayTipText;
                }
                else
                {
                    DownloadFileHelper.DownFileProgressIntChanged -= DownFileProgressIntChanged;
                    displayTipText = "解压中...";
                    e.Order.showValue = displayTipText;
                }
            }
            else
            {
                DownloadFileHelper.DownFileProgressIntChanged -= DownFileProgressIntChanged;
                if ((!string.IsNullOrWhiteSpace(e.FilePath)) &&
                    StringPath.PathExists(e.FilePath))
                    File.Delete(e.FilePath);

                MessageForm.Show("下载过程中出现错误," + e.ErrorMessage);
            }
        }

        /// <summary>
        /// 复制文件进度显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Instance_CopyFileProgressIntChanged(object sender, CopyFileEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.ErrorMessage))
            {
                string displayTipText = string.Empty;
                if (!e.IsSuccessful)
                {
                    displayTipText = string.Format("复制:{0:f2}%", e.Percentage * 100);
                    e.Order.showValue = displayTipText;
                }
                else
                {
                    FileCopyHelper.Instance.CopyFileProgressIntChanged -= Instance_CopyFileProgressIntChanged;
                    displayTipText = "复制成功";

                    e.Order.showValue = displayTipText;

                    //复制成功之后压缩
                    var zipPath = Path.GetDirectoryName(e.FilePath) + "\\" + e.Order.OrderID + ".zip";
                    if (StringPath.PathExists(zipPath))
                        File.Delete(zipPath);
                    ZipFileHelper.Instance.ZipFileProgressIntChanged += ZipFileProgressIntChanged;
                    ZipFileHelper.Instance.ZipFileWithProgress(e.FilePath, zipPath, e.Order);
                }
            }
            else
            {
                FileCopyHelper.Instance.CopyFileProgressIntChanged -= Instance_CopyFileProgressIntChanged;
                if ((!string.IsNullOrWhiteSpace(e.FilePath)) &&
                    StringPath.PathExists(e.FilePath))
                    File.Delete(e.FilePath);
                MessageForm.Show("复制过程中出现错误," + e.ErrorMessage);
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 刷新页面
        /// </summary>
        internal void RefreshViewData()
        {
            //将分页模型转换为数据库请求模型
            var requestPaginationModel = _pagination.ToDb() as GetMyOrderRequestModel;
            //过滤模型与分页模型合并
            _requestModel.MaxResultCount = requestPaginationModel.MaxResultCount;
            _requestModel.SkipCount = requestPaginationModel.SkipCount;
            //根据合并后的请求模型请求订单数据
            var list = RequestData(_requestModel);
            //刷新数据
            RefreshData(list);
            OrderRecordPaginationView.OrderRecordPaginationViewModel = _pagination;
        }
        #endregion
    }
}
