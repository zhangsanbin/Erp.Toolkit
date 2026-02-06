/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-13           Andy        Split, restructure
 * 2025-08-21           Lele        Subscriber Check
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    public partial class Dgv
    {
        #region 自定义事件

        #region 事件委托

        /// <summary>
        /// 触发刷新数据请求，事件
        /// </summary>
        [Description("触发刷新数据请求，事件")]
        public event RefreshEventHandler RefreshDgv;

        /// <summary>
        /// 触发选择 DGV 不同行或单元格，事件
        /// </summary>
        [Description("触发选择 DGV 不同行或单元格，事件")]
        public event EventByIdHandler SelectionChangedDgv;

        /// <summary>
        /// 触发单击 DGV 中某一行，事件
        /// </summary>
        [Description("触发单击 DGV 中某一行，事件")]
        public event EventByIdHandler ClickDgv;

        /// <summary>
        /// 触发双击 DGV 中某一行，事件
        /// </summary>
        [Description("触发双击 DGV 中某一行，事件")]
        public event EventByIdHandler DoubleClickDgv;

        /// <summary>
        /// 触发添加一行，事件
        /// </summary>
        [Description("触发添加一行，事件")]
        public event EventBaseHandler AddDgv;

        /// <summary>
        /// 触发修改 DGV 中某一行，事件
        /// </summary>
        [Description("触发修改 DGV 中某一行，事件")]
        public event EditEventHandler EditDgv;

        /// <summary>
        /// 触发删除 DGV 中某一行，事件
        /// </summary>
        [Description("触发删除 DGV 中某一行，事件")]
        public event DeleteEventHandler DeleteDgv;

        /// <summary>
        /// 触发打印，事件
        /// </summary>
        [Description("触发打印，事件")]
        public event PrintEventByIdsHandler PrintDgv;

        /// <summary>
        /// 触发导出，事件
        /// </summary>
        [Description("触发导出，事件")]
        public event ExportEventByIdsHandler ExportDgv;

        /// <summary>
        /// 触发主从数据展开，事件
        /// </summary>
        [Description("触发主从数据展开，事件")]
        public event MasterSlaveDataHandler MasterSlaveDataExpand;

        /// <summary>
        /// 触发主从数据折叠，事件
        /// </summary>
        [Description("触发主从数据折叠，事件")]
        public event MasterSlaveDataHandler MasterSlaveDataCollapse;

        /// <summary>
        /// 基础，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("基础，事件处理程序委托")]
        public delegate void EventBaseHandler(object sender, EventArgs e);

        /// <summary>
        /// 刷新，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("刷新，事件处理程序委托")]
        public delegate void RefreshEventHandler(object sender, DgvRefreshEventArgs e);

        /// <summary>
        /// 附带选择 Id ，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("附带选择 Id ，事件处理程序委托")]
        public delegate void EventByIdHandler(object sender, EventArgs e, string id);

        /// <summary>
        /// 删除事件（可覆盖），附带选择 Id ，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("删除事件（可覆盖），附带选择 Id ，事件处理程序委托")]
        public delegate void DeleteEventHandler(object sender, DgvDeleteEventArgs e, string id);

        /// <summary>
        /// 附带选择 Ids ，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("附带选择 Ids ，事件处理程序委托")]
        public delegate void EventByIdsHandler(object sender, EventArgs e, string ids);

        /// <summary>
        /// 打印事件（可覆盖），附带选择 Ids ，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("打印事件（可覆盖），附带选择 Ids ，事件处理程序委托")]
        public delegate void PrintEventByIdsHandler(object sender, DgvPrintEventArgs e, string ids);

        /// <summary>
        /// 导出事件（可覆盖），附带选择 Ids ，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("导出事件（可覆盖），附带选择 Ids ，事件处理程序委托")]
        public delegate void ExportEventByIdsHandler(object sender, DgvExportEventArgs e, string ids);

        /// <summary>
        /// 触发主从数据，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <param name="rect"></param>
        [Description("触发主从数据，事件处理程序委托")]
        public delegate void MasterSlaveDataHandler(object sender, DataGridViewCellMouseEventArgs e, string id, Rectangle rect);

        /// <summary>
        /// 修改，事件处理程序委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="PrimaryKey">主键列名</param>
        /// <param name="PrimaryKeyValue">主键值</param>
        /// <param name="cellName">修改列名</param>
        /// <param name="newValue">新值</param>
        /// <param name="typeName">值类型</param>
        [Description("修改，事件处理程序委托")]
        public delegate void EditEventHandler(object sender, EventArgs e, string PrimaryKey, string PrimaryKeyValue, string cellName, string newValue, Type typeName);

        #endregion 事件委托

        #region 事件逻辑和绑定触发

        #region 按钮状态管理

        // 更新按钮的启用状态
        private void UpdateButtonState()
        {
            // 检查删除事件是否有订阅者
            bool hasSubscribersDelete = DeleteDgv != null && DeleteDgv.GetInvocationList().Length > 0;

            // 检查新增事件是否有订阅者
            bool hasSubscribersAdd = AddDgv != null && AddDgv.GetInvocationList().Length > 0;

            // 检查刷新事件是否有订阅者
            bool hasSubscribersReLoadList = RefreshDgv != null && RefreshDgv.GetInvocationList().Length > 0;

            // 导航栏删除按钮
            bindingNavigatorDeleteItem.Enabled = hasSubscribersDelete;
            ToolStripMenuItem_del.Enabled = hasSubscribersDelete;

            // 导航栏新增按钮
            bindingNavigatorAddNewItem.Enabled = hasSubscribersAdd;
            ToolStripMenuItem_add.Enabled = hasSubscribersAdd;

            // 工具栏刷新按钮
            toolStripButton_ReLoadList.Enabled = hasSubscribersReLoadList || _owner != null;
            // 导航栏刷新按钮
            toolStripButtonReLoadList.Enabled = hasSubscribersReLoadList || _owner != null;
            // 菜单栏刷新按钮
            ToolStripMenuItem_refresh.Enabled = hasSubscribersReLoadList || _owner != null;
        }

        // 外部调用，以更新按钮状态
        public void RefreshButtonState()
        {
            UpdateButtonState();
        }

        #endregion 按钮状态管理

        /// <summary>
        /// 触发刷新数据请求，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnRefreshDgv(object sender, DgvRefreshEventArgs e)
        {
            // 刷新前，判断并关闭已经展开的子视图
            if (!(_subviewCurrentRow.Count == 0) && subview != null && subview.Visible)
            {
                // 如果有其他行已经展开，先折叠它
                var eRow = _subviewCurrentRow[0];
                _subviewCurrentRow.Clear();
                dataGridView.Rows[eRow].Height = dataGridView.RowTemplate.Height;
                dataGridView.Rows[eRow].DividerHeight = 0;
                dataGridView.ClearSelection();
                dataGridView.Rows[eRow].Selected = true;
                subview.Visible = false;
            }

            // 刷新事件，允许外部逻辑处理事件
            RefreshDgv?.Invoke(sender, e);

            if (!e.Handled && _owner != null)
            {
                // 调用父类实现的接口，刷新方法
                _owner.LoadLists();
            }
        }

        /// <summary>
        /// 触发选择 DGV 不同行或单元格，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        protected virtual void OnSelectionChangedDgv(object sender, EventArgs e, string id)
        {
            SelectionChangedDgv?.Invoke(sender, e, id);
        }

        /// <summary>
        /// 触发单击 DGV 中某一行，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        protected virtual void OnClickDgv(object sender, EventArgs e, string id)
        {
            ClickDgv?.Invoke(sender, e, id);
        }

        /// <summary>
        /// 触发双击 DGV 中某一行，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        protected virtual void OnDoubleClickDgv(object sender, EventArgs e, string id)
        {
            DoubleClickDgv?.Invoke(sender, e, id);
        }

        /// <summary>
        /// 触发新增一行，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnAddDgv(object sender, EventArgs e)
        {
            AddDgv?.Invoke(sender, e);
        }

        /// <summary>
        /// 修改，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="PrimaryKey">主键列名</param>
        /// <param name="PrimaryKeyValue">主键值</param>
        /// <param name="cellName">修改列名</param>
        /// <param name="newValue">新值</param>
        /// <param name="typeName">值类型</param>
        protected virtual void OnEditDgv(object sender, EventArgs e, string PrimaryKey, string PrimaryKeyValue, string cellName, string newValue, Type typeName)
        {
            EditDgv?.Invoke(sender, e, PrimaryKey, PrimaryKeyValue, cellName, newValue, typeName);
        }

        /// <summary>
        /// 触发删除 DGV 中某一行，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        protected virtual void OnDeleteDgv(object sender, DgvDeleteEventArgs e, string id)
        {
            DeleteDgv?.Invoke(sender, e, id);

            // 检查事件，已经被处理后逻辑
            if (e.Handled)
            {
                // 前端已经完成数据校验和删除流程后
                // 内置方法执行数据清理和同步逻辑
                // Todo...
                Console.WriteLine("Data cleaning...");
            }
        }

        /// <summary>
        /// 触发打印，默认的事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <remarks>
        /// <para>覆盖基础逻辑方法：</para>
        /// <list type="bullet">
        /// <item>步骤1：给 <paramref name="dataGridView"/> 手动绑定 PrintDgv 事件。</item>
        /// <item>步骤2：实现 PrintDgv 处理方法。</item>
        /// </list>
        /// <para>Code：</para>
        /// <code>
        /// dgv1.PrintDgv += MyDgvPrintEvent;
        /// ...
        /// </code>
        /// </remarks>
        protected virtual void OnPrintDgv(object sender, DgvPrintEventArgs e, string ids)
        {
            // 触发事件，允许外部逻辑处理事件
            PrintDgv?.Invoke(sender, e, ids);

            // 检查事件是否被处理
            if (!e.Handled)
            {
                // 执行默认的基础逻辑
                Console.WriteLine("Executing default print logic...");
                _printDocument.DocumentName = e.Title;
                _printDocument.DefaultPageSettings.Margins.Top = 50;  // 顶部边距
                _printDocument.DefaultPageSettings.Margins.Bottom = 30; // 底部边距
                _printDocument.DefaultPageSettings.Margins.Left = 30; // 左侧边距
                _printDocument.DefaultPageSettings.Margins.Right = 30; // 右侧边距
                _printDialog.Document = _printDocument;
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    _printDocument.Print();
                }
            }
        }

        /// <summary>
        /// 触发导出，默认的事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <remarks>
        /// <para>覆盖基础逻辑方法：</para>
        /// <list type="bullet">
        /// <item>步骤1：给 <paramref name="dataGridView"/> 手动绑定 ExportDgv 事件。</item>
        /// <item>步骤2：实现 ExportDgv 处理方法。</item>
        /// </list>
        /// <para>Code：</para>
        /// <code>
        /// dgv1.ExportDgv += MyDgvExpEvent;
        /// ...
        /// </code>
        /// </remarks>
        protected virtual void OnExportDgv(object sender, DgvExportEventArgs e, string ids)
        {
            // 触发事件，允许外部逻辑处理事件
            ExportDgv?.Invoke(sender, e, ids);

            // 检查事件是否被处理
            if (!e.Handled)
            {
                // 执行默认的基础逻辑
                Console.WriteLine("Executing default Export logic...");

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Save a CSV File";

                // 用户点击了"保存"按钮
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false))
                        {
                            // 写入列标题
                            for (int i = 0; i < dataGridView.Columns.Count; i++)
                            {
                                if (i > 0) sw.Write(",");
                                sw.Write(dataGridView.Columns[i].HeaderText.Replace(",", ";")); // 避免逗号在标题中引起的问题
                            }
                            sw.WriteLine();

                            // 写入数据
                            foreach (DataGridViewRow row in dataGridView.Rows)
                            {
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    if (i > 0) sw.Write(",");

                                    // 处理可能的逗号或换行符，这些在CSV中需要转义
                                    string cellValue = row.Cells[i].Value?.ToString() ?? "";
                                    cellValue = cellValue.Replace(",", ";").Replace(Environment.NewLine, " ");
                                    sw.Write(cellValue);
                                }
                                sw.WriteLine();
                            }
                        }

                        MessageBox.Show("数据已成功导出到CSV文件！");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出CSV文件时发生错误：" + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 触发主从数据展开，方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <param name="rect"></param>
        protected virtual void OnMasterSlaveDataExpand(object sender, DataGridViewCellMouseEventArgs e, string id, Rectangle rect)
        {
            var dgv_sender = (System.Windows.Forms.DataGridView)sender;

            if (!(_subviewCurrentRow.Count == 0) && dgv_sender != null)
            {
                // 计算，主从数据窗口位置
                Rectangle rect2 = dgv_sender.GetCellDisplayRectangle(GetFirstVisibleColumnIndex(), _subviewCurrentRow[0], false);

                // 修正，显示位置(由于上一步 GetCellDisplayRectangle 方法中，给的 columnindex = 0 ，实际上如果第一列不可见，或水平滚动后被遮挡会出现错乱)
                if (rect2.X == 0) rect2.X = dgv_sender.Location.X + 48;

                // 先隐藏子视图
                subview.Visible = false;

                // 显示子视图
                subview.Location = new Point(rect2.X, rect2.Y + dgv_sender.RowTemplate.Height + dgv_sender.Location.Y);
                subview.Width = dgv_sender.Width - dgv_sender.RowHeadersWidth - 20;
                subview.Height = (dgv_sender.Height / 2);

                // 处理子视图在最底部时，部分UI遮挡时，滚动DGV逻辑，让子视图完整显示出来
                if (subview.Location.Y > (dgv_sender.Height / 2))
                {
                    // 计算需要滚动几行，才能满足子视图完整的显示在底部
                    int num = (subview.Location.Y - subview.Height) / dgv_sender.RowTemplate.Height;

                    // 向下滚动，指定行数
                    ScrollDown(num);
                }

                // 显示子数据
                subview.Visible = true;
            }

            MasterSlaveDataExpand?.Invoke(this, e, id, rect);
        }

        /// <summary>
        /// 触发主从数据折叠，方法
        /// </summary>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <param name="rect"></param>
        protected virtual void OnMasterSlaveDataCollapse(DataGridViewCellMouseEventArgs e, string id, Rectangle rect)
        {
            // 关闭子数据
            subview.Visible = false;

            // 级联关闭子数据
            if (subview.subview != null)
            {
                subview.subview.Visible = false;
                // 级联关闭子数据（临时四层）
                if (subview.subview.subview != null) subview.subview.subview.Visible = false;
            }

            subview.dataGridView.DataSource = null;

            subview._bindingSource = null;

            MasterSlaveDataCollapse?.Invoke(this, e, id, rect);
        }

        /// <summary>
        /// 向下滚动，指定行数
        /// </summary>
        /// <param name="rowCount"></param>
        private void ScrollDown(int rowCount)
        {
            int currentFirstRow = dataGridView.FirstDisplayedScrollingRowIndex;
            int newFirstRow = currentFirstRow + rowCount;

            // 确保不会超出总行数
            if (newFirstRow < dataGridView.Rows.Count)
            {
                dataGridView.FirstDisplayedScrollingRowIndex = newFirstRow;
            }
            else
            {
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
            }
        }

        /// <summary>
        /// 向上滚动，指定行数
        /// </summary>
        /// <param name="rowCount"></param>
        private void ScrollUp(int rowCount)
        {
            int currentFirstRow = dataGridView.FirstDisplayedScrollingRowIndex;
            int newFirstRow = currentFirstRow - rowCount;

            // 确保不会小于0
            if (newFirstRow >= 0)
            {
                dataGridView.FirstDisplayedScrollingRowIndex = newFirstRow;
            }
            else
            {
                dataGridView.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        /// <summary>
        /// 获取 DataGridView 中第一个没有被隐藏的列的索引
        /// </summary>
        /// <returns>第一个可见列的索引</returns>
        private int GetFirstVisibleColumnIndex()
        {
            // 按显示顺序（DisplayIndex）排序后查找第一个可见列
            foreach (DataGridViewColumn column in dataGridView.Columns
                .Cast<DataGridViewColumn>()
                .OrderBy(c => c.DisplayIndex))
            {
                if (column.Visible)
                {
                    // 返回实际索引（非 DisplayIndex）
                    return column.Index;
                }
            }
            return -1; // 无可见列
        }

        /// <summary>
        /// 新建行，事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            OnAddDgv(sender, e);
        }

        /// <summary>
        /// 删除行，事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            // 响应事件
            DgvDeleteEventArgs args = new DgvDeleteEventArgs(GetSelectedItemIds());
            OnDeleteDgv(sender, args, GetSelectedItemIds());
        }

        /// <summary>
        /// 编辑后，事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 检查事件是否有订阅者
            bool hasSubscribers = EditDgv != null && EditDgv.GetInvocationList().Length > 0;

            if (!hasSubscribers)
            {
                // 如果没有订阅者，直接返回
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // 获取触发事件的行数据
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                object value = row.Cells[_primaryKey].Value;
                string primaryKeyCellValue = value?.ToString();
                if (string.IsNullOrEmpty(primaryKeyCellValue))
                {
                    // 主键的数据Id为空时直接返回退出
                    return;
                }

                // 列名称
                string cellName = dataGridView.Columns[e.ColumnIndex].Name.ToString();
                // 获取更改后的值
                string newValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                // 列数据类型
                Type typeName = dataGridView.Columns[e.ColumnIndex].ValueType;

                // 响应事件
                OnEditDgv(sender, e, PrimaryKey, primaryKeyCellValue, cellName, newValue, typeName);
            }
        }

        /// <summary>
        /// 选择不同行或单元格时立即触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // 当选中行变化时实时更新显示
            UpdateStatisticsAndSelectionInfo();

            // 检查事件是否有订阅者
            bool hasSubscribers = SelectionChangedDgv != null && SelectionChangedDgv.GetInvocationList().Length > 0;

            if (!hasSubscribers)
            {
                // 如果没有订阅者，直接返回
                return;
            }

            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];
                object value = row.Cells[_primaryKey].Value;
                string primaryKeyCellValue = value?.ToString();
                if (string.IsNullOrEmpty(primaryKeyCellValue))
                {
                    // 主键的数据Id为空时直接返回退出
                    return;
                }

                // 响应事件
                OnSelectionChangedDgv(sender, e, primaryKeyCellValue);
            }
        }

        /// <summary>
        /// 鼠标在 DataGridView 中的单元格单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 检查事件是否有订阅者
            bool hasSubscribers = ClickDgv != null && ClickDgv.GetInvocationList().Length > 0;

            if (!hasSubscribers)
            {
                // 如果没有订阅者，直接返回
                return;
            }

            if (e.Button == MouseButtons.Left && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                // 获取触发事件的行数据
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                object value = row.Cells[_primaryKey].Value;
                string primaryKeyCellValue = value?.ToString();
                if (string.IsNullOrEmpty(primaryKeyCellValue))
                {
                    // 主键的数据Id为空时直接返回退出
                    return;
                }

                // 响应事件
                OnClickDgv(sender, e, primaryKeyCellValue);
            }
        }

        /// <summary>
        /// 鼠标在 DataGridView 中的单元格双击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 检查事件是否有订阅者
            bool hasSubscribers = DoubleClickDgv != null && DoubleClickDgv.GetInvocationList().Length > 0;

            if (!hasSubscribers)
            {
                // 如果没有订阅者，直接返回
                return;
            }

            if (e.Button == MouseButtons.Left && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                // 获取触发事件的行数据
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                object value = row.Cells[_primaryKey].Value;
                string primaryKeyCellValue = value?.ToString();
                if (string.IsNullOrEmpty(primaryKeyCellValue))
                {
                    // 主键的数据Id为空时直接返回退出
                    return;
                }

                // 响应事件
                OnDoubleClickDgv(sender, e, primaryKeyCellValue);
            }
        }

        #endregion 事件逻辑和绑定触发

        #endregion 自定义事件
    }
}