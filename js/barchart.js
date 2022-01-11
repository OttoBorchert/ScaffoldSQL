class BarChart {

    /**
     * @param {string} id Name of the SVG element to control.
     * @param {string} ledgend Name of the SVG element that will serve as the ledgend.
     */
    constructor(id, ledgend) {
        this.svg = document.getElementById(id);
        this.ledgend = document.getElementById(ledgend);
        // Error checking.
        if (!id || !ledgend) throw "Unable to instantiate barchart: id or ledgend is invalid.";
        this.clear();
    }

    /**
     * Set the class for a columns type. Note: this must be called BEFORE render.
     * @param {string} cssClass What class should be used?
     * @param {string} column What column should have this class applied to?
     */
    setCssClassColumn(cssClass, column = '') { this.subColCssClass[column] = cssClass; }

    /**
     * Set the data within a column and subgroup to have a specified value.
     * @param {number} value The numerical value of the data.
     * @param {string | number} column What column should be writen too?
     * @param {string | number} group If there are groups of columns, what gorup should be written to?
     */
    setData(value, column = '', group = '',) {
        if (!(column in this.data)) {
            this.data[column] = {};
        }
        if (!(group in this.data[column])) {
            ++this.cols;
            if (!(group in this.groups)) {
                this.groups[group] = group;
            }
        }
        this.data[column][group] = value;
    }

    /**
     * Render the barchart onto the twin SVG's.
     * @param {number} horizontalLines Howmany lines should be drawn horizontally on the chart?
     * @param {number} distBetweenGroups What should the distance between column groups be?
     * @param {number} distBetweenCols What should be the distance between columns be?
     * @param {number} colWidth What should the width of the column be?
     * @param {boolean} showLedgend Should the SVG for the ledgend be written too?
     * @param {boolean} partialValues Should the lines on the barchart be able to map to partial values, i.e., should the labels exclude 1.2, 2.2, 3.3, and so on?
     * @param {number} maxheight What percentage of the svg should the barchart take up? Maps to the range [0, 1].
     */
    render(horizontalLines = 10, distBetweenGroups = 5, distBetweenCols = 2, colWidth = 8, showLedgend = true, partialValues = false, maxheight = .8) {
        this.#clearSVG();
        let max = this.#getMax();
        // Create the child groups.
        let chartGroup = document.createElementNS('http://www.w3.org/2000/svg', 'g');
        let lineGroup = document.createElementNS('http://www.w3.org/2000/svg', 'g');
        // Write to the SVG it's child groups.
        this.svg.appendChild(lineGroup);
        this.svg.appendChild(chartGroup);
        // Write to the child groups.
        this.#createGroups(max, distBetweenGroups, distBetweenCols, colWidth, chartGroup, maxheight);
        this.#createGuideLines(horizontalLines, max, lineGroup, partialValues, maxheight);
        if (showLedgend) {
            this.#createLedgend(this.ledgend);
        }
    }

    /**
     * Create the horizontal lines.
     * @param {number} lines The number of lines.
     * @param {number} max The maximum value in the barchart.
     * @param {SVGElement} lineGroup The group the lines will be written too.
     * @param {boolean} partial Allow nonwhole numbers?
     * @param {number} maxheight Maximum hiehgt to write to, as a percentage.
     */
    #createGuideLines(lines, max, lineGroup, partial, maxheight) {
        // If lines < max, set to max.
        lines = Math.ceil(Math.min(lines, max));
        if (lines < 1) {
            return;
        }
        lineGroup.classList.add('barchartrow');
        let data = [0];
        // For each in range 1..lines-2
        for (let i = 1; i <= lines - 2; ++i) {
            let portion = i / lines;
            // If partial is not enabled, round to nearest value
            if (!partial) {
                let d = Math.round(max * portion);
                portion = d / max;
            }
            data.push(portion);
        }
        data.push(1);
        for (let i = 0; i < data.length; ++i) {
            let d = data[i];
            let lineGroupLcl = document.createElementNS('http://www.w3.org/2000/svg', 'g');
            let text = document.createElementNS('http://www.w3.org/2000/svg', 'text');
            text.textContent = partial ? max * d : Math.round(max * d);
            let y = this.#map(d, 0, 1, 0, maxheight);
            let line = this.#createLine(`${y * 100}%`);
            text.setAttribute('y', `${-y * 100}%`);
            text.setAttribute('x', -10);
            lineGroupLcl.appendChild(text);
            lineGroupLcl.appendChild(line);
            lineGroup.appendChild(lineGroupLcl);
        }
    }

    /**
     * Create a horizontal line at a specified hieght.
     * @param {string | number} y
     */
    #createLine(y) {
        let line = document.createElementNS('http://www.w3.org/2000/svg', 'line');
        line.setAttribute('x1', 0);
        line.setAttribute('y1', y);
        line.setAttribute('x2', '100%');
        line.setAttribute('y2', y);
        return line;
    }

    /**
     * Create each group of columns.
     * @param {number} max The maximum value
     * @param {number} distBetweenGroups Space between each column group.
     * @param {number} distBetweenCols Space between each column.
     * @param {number} colWidth The width of each column.
     * @param {SVGElement} chartGroup The group that contains the bars of the barchart.
     * @param {number} maxheight The maximum hieght, in percentage, to use in the SVG.
     */
    #createGroups(max, distBetweenGroups, distBetweenCols, colWidth, chartGroup, maxheight) {
        let width = 0;
        let lastCol = false;
        chartGroup.classList.add('barchartcolumn');
        let keys = Object.keys(this.data).sort();
        for (let colGroup of keys) {
            let group = document.createElementNS('http://www.w3.org/2000/svg', 'g');
            chartGroup.appendChild(group);
            let text = document.createElementNS('http://www.w3.org/2000/svg', 'text');
            text.setAttribute("dy", "1em");
            text.classList.add('columnLabel');
            text.textContent = colGroup;
            if (lastCol) {
                width += distBetweenGroups;
            } else {
                lastCol = true;
            }
            group.setAttribute("transform", `translate(${width} 0)`);
            group.appendChild(text);
            width += this.#createColumns(max, distBetweenCols, colWidth, group, colGroup, maxheight);
        }
    }

    /**
     * Create each individual column.
     * @param {number} max The maximum value in the data table.
     * @param {number} distBetweenCols The distance between columns.
     * @param {number} colWidth The width of the column.
     * @param {SVGElement} group The group element that will be written to.
     * @param {string} colGroup What is the name of the group?
     * @param {number} maxheight The maximum height to write to, as a percentage.
     * @returns {number} The width taken up.
     */
    #createColumns(max, distBetweenCols, colWidth, group, colGroup, maxheight) {
        let lclwidth = 0;
        for (let colID in this.data[colGroup]) {
            let col = document.createElementNS('http://www.w3.org/2000/svg', 'rect');
            col.setAttribute("height", `${this.#map(this.data[colGroup][colID], 0, max, 0, maxheight) * 100}%`);
            col.setAttribute("width", colWidth);
            col.setAttribute("transform", `translate(${lclwidth} 0)`)
            col.setAttribute("name", "BarchartJS" + colID);
            lclwidth += colWidth + distBetweenCols;
            if (this.subColCssClass[colID]) {
                col.classList.add(this.subColCssClass[colID]);
            }
            group.appendChild(col);
        }
        return lclwidth;
    }

    /**
     * Create the ledgend for the barchart.
     * @param {SVGElement} ledgendGroup The SVG the ledgend will be written to.
     */
    #createLedgend(ledgendGroup) {
        ledgendGroup.classList.add('barchartledgend');
        const textSize = parseInt(window.getComputedStyle(document.querySelector('.barchartLedgend'))['fontSize']);
        let pos = 0;
        for (let g in this.groups) {
            if (g != '') {
                let group = document.createElementNS('http://www.w3.org/2000/svg', 'g');
                group.setAttribute('transform', `translate(0, ${pos})`)
                pos += textSize / 2;
                ledgendGroup.appendChild(group);
                let text = document.createElementNS('http://www.w3.org/2000/svg', 'text');
                text.setAttribute("dx", `1.5em`);
                let colorbox = document.createElementNS('http://www.w3.org/2000/svg', 'rect');
                colorbox.setAttribute('transform', `translate(0, ${-textSize / 4})`);
                group.appendChild(text);
                group.appendChild(colorbox);
                let colorborder, e = getComputedStyle(this.svg.querySelector(`rect[name='BarchartJS${g}'`));
                colorborder = e['stroke'];
                colorbox.setAttribute('fill', e['fill']);
                if (colorborder) {
                    colorbox.setAttribute('stroke', colorborder);
                }
                text.textContent = g;
            }
        }
    }

    /**
     * Get the max value in the barchart
     **/
    #getMax() {
        let max = Number.MIN_VALUE;
        for (let group in this.data) {
            for (let col in this.data[group]) {
                max = Math.max(max, this.data[group][col]);
            }
        }
        return max;
    }

    /**
     * Map a value from one range, onto another. Got the idea from the Arduino standard libraries.
     * Source: https://stackoverflow.com/questions/5731863/mapping-a-numeric-range-onto-another
     * @param {any} value The value. Must be between inputStart..inputEnd
     * @param {any} inputStart The start of the input range.
     * @param {any} inputEnd The end of the input range.
     * @param {any} outputStart The start of the output range.
     * @param {any} outputEnd The end of the output range.
     */
    #map(value, inputStart, inputEnd, outputStart, outputEnd) {
        // I found this function usefull for Arduino programming. Thought it was usefull here to.
        // https://stackoverflow.com/questions/5731863/mapping-a-numeric-range-onto-another
        // Error checking.
        if (inputStart > inputEnd) throw `Expected inputStart < inputEnd. Got ${inputStart} < ${inputEnd}`;
        if (outputStart > outputEnd) throw `Expected outputStart < outputEnd. Got ${outputStart} < ${outputEnd}`;
        // Enusre that value maps to range.
        value = Math.max(Math.min(value, inputEnd), inputStart);
        let inputRange = inputEnd - inputStart;
        let outputRange = outputEnd - outputStart;
        return (value - inputStart) * outputRange / inputRange + outputStart;
    }

    /**
     * Delete data from all controlled SVG's. 
     */
    #clearSVG() {
        // Delete all child nodes
        for (let i = this.svg.children.length; i > 0; --i) {
            this.svg.children[0].remove();
        }
        for (let i = this.ledgend.children.length; i > 0; --i) {
            this.ledgend.children[0].remove();
        }
    }

    /**
     * Delete all internal data and clear SVG's so they may be written to.
     **/
    clear() {
       this.#clearSVG();
       this.subColCssClass = {};
       this.groups = {};
       this.data = {};
       this.cols = 0;
    }

}