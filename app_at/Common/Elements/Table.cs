using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Common.Elements
{
    /// <summary>
    /// Table custom Element
    /// </summary>
    public class Table : BaseElement
    {
        private const int OPTIMAL_RIGHT_X_SHIFTING = 100;
        private const int OPTIMAL_LEFT_X_SHIFTING = -100;
        private const int MAX_NUMBER_OF_PIXELS_BETWEEN_SCROLL_AND_RIGHT_ARROW = 5;
        private const int MAX_COUNT_OF_SCROLL_ITERATIONS = 100;

        private readonly AppiumWebElement _table;
        private AppiumWebElement _hScroller;
        private AppiumWebElement _vScroller;
        private AppiumWebElement _hScrollerRightArrow;
        private AppiumWebElement _hScrollerLeftArrow;
        private AppiumWebElement _vScrollerDownArrow;
        private AppiumWebElement _vScrollerUpArrow;
        private ReadOnlyCollection<AppiumWebElement> _scrollers;

        /// <summary>
        /// Create new exemplar of class using By locator
        /// </summary>
        /// <param name="by">By locator</param>
        public Table(By by) : base()
        {
            _table = ElementDriver.FindElement(by);
            InitAllScrollItems();
        }

        /// <summary>
        /// Get value of cell
        /// </summary>
        /// <param name="column">Column number</param>
        /// <param name="row">Row number </param>
        /// <returns> Returns string value of cell</returns>
        public string GetCellValue(string column, int row)
        {
            AppiumWebElement headerCell = _table.FindElementByName(column);
            SetCellVisible(headerCell);
            AppiumWebElement filterCell = _table.FindElementByName(column + " row " + row);
            return filterCell.Text;
        }

        /// <summary>
        /// Do one click on cell
        /// </summary>
        /// <param name="column">Column number</param>
        /// <param name="row">Row number</param>
        public void ClickOnCell(string column, int row)
        {
            AppiumWebElement headerCell = _table.FindElementByName(column);
            SetCellVisible(headerCell);
            AppiumWebElement cell = _table.FindElementByName(column + " row " + row);
            cell.ClickAction();
        }

        /// <summary>
        /// Do one click on the row
        /// Row number starts with 1
        /// </summary>
        /// <param name="row">Row number</param>
        public void ClickOnRow(int row)
        {
            AppiumWebElement cell = _table.FindElementByName("Row " + row);
            SetCellVisible(cell);
            cell.ClickAction();
        }

        public void ClickOnCell(string column)
        {
            AppiumWebElement cell = _table.FindElementByName(column);
            SetCellVisible(cell);
            cell.ClickAction();
        }

        /// <summary>
        /// Do double click on cell
        /// </summary>
        /// <param name="column">Column number</param>
        /// <param name="row">Row number</param>
        public void DoubleClickOnCell(string column, int row)
        {
            AppiumWebElement headerCell = _table.FindElementByName(column);
            SetCellVisible(headerCell);
            AppiumWebElement cell = _table.FindElementByName(column + " row " + row);
            cell.DoubleClickAction();
        }

        /// <summary>
        /// Do multiple select
        /// Click on all row.
        /// </summary>
        public void SelectAllRows()
        {
            AppiumWebElement cell = _table.FindElementByName("Row " + 1);
            SetCellVisible(cell);
            cell.ClickAction();
            cell.SendKeys(Keys.Control + "a");
        }

        /// <summary>
        /// Set the Value of filter
        /// </summary>
        /// <param name="column">Column number</param>
        /// <param name="value">Value of Filter</param>
        public void SetFilterCell(string column, string value)
        {
            AppiumWebElement headerCell = _table.FindElementByName(column);
            SetCellVisible(headerCell);

            AppiumWebElement filterCell = _table.FindElementByXPath($"//Custom[@Name='Filter Row']/DataItem[starts-with(@Name, '{column}')]");
            filterCell.SetValue(value);
        }


        /// <summary>
        /// Set non filter cell by value
        /// </summary>
        /// <param name="column">Column number</param>
        /// <param name="row">Row number</param>
        /// <param name="value">String Value</param>
        public void SetCellValue(string column, int row, string value)
        {
            AppiumWebElement headerCell = _table.FindElementByName(column);
            SetCellVisible(headerCell);
            AppiumWebElement cell = _table.FindElementByName(column + " row " + row);
            cell.SetValue(value);
        }

        /// <summary>
        /// Set some filters in a table
        /// </summary>
        /// <param name="filter">Dictionary in format [NameOfColumn] [ValueOfColumn]</param>
        public void SetFilters(Dictionary<string, string> filter)
        {
            foreach (string key in filter.Keys)
            {
                if (filter[key] != null)
                {
                    SetFilterCell(key, filter[key]);
                }
            }
        }

        public void SetParameters(Dictionary<string, string> filter)
        {
            foreach (string key in filter.Keys)
            {
                if (filter[key] != null)
                {
                    SetCellValue(key, filter[key]);
                }
            }
        }

        /// <summary>
        /// Clear a cell
        /// </summary>
        /// <param name="column">Column number</param>
        /// <param name="row">Row number</param>
        public void ClearCell(string column, int row)
        {
            AppiumWebElement headerCell = _table.FindElementByName(column);
            SetCellVisible(headerCell);
            AppiumWebElement cell = _table.FindElementByName(column + " row " + row);
            cell.CleanInputAction();
        }

        /// <summary>
        /// This method find and initiate all items which can be used for working with table(Vertical scroll, Horizontal scroll, Right/Left arrows)
        /// </summary>
        private void InitAllScrollItems()
        {
            _scrollers = _table.FindElementsByXPath("//ScrollBar[@Name='scroll bar']/Thumb[@Name='Position']");
            if (_scrollers.Count != 0)
            {
                foreach (AppiumWebElement element in _scrollers)
                {
                    if (element.Rect.Width > element.Rect.Height)
                    {
                        _hScroller = element;
                    }
                    else
                    {
                        _vScroller = element;
                    }
                }
            }

            if (_hScroller != null)
            {
                _hScrollerRightArrow = _table.FindElementByXPath("//ScrollBar[@Name='scroll bar']/Button[@Name='Column Right']");
                _hScrollerLeftArrow = _table.FindElementByXPath("//ScrollBar[@Name='scroll bar']/Button[@Name='Column Left']");
            }

            if (_vScroller != null)
            {
                _vScrollerDownArrow = _table.FindElementByXPath("//ScrollBar[@Name='scroll bar']/Button[@Name='Line Down']");
                _vScrollerUpArrow = _table.FindElementByXPath("//ScrollBar[@Name='scroll bar']/Button[@Name='Line Up']");
            }
        }
    }
}
