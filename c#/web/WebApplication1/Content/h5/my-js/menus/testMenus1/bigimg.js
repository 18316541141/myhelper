controller('m13', 'menus/testMenus1/bigImg.html', {
    data() { return { tableData: [] }; },
    mounted() {
        this.tableData = [
            { img: '/index/showImage?pathName=test&imgName=2cc06f1bffa45ad0fa9c22474081ed0d253a7d63', bigImg:'/index/showImage?pathName=test&imgName=5dbc68f5ab19c9bd74dc21b15fd360e86151c65a' },
            { img: '/index/showImage?pathName=test&imgName=2cc06f1bffa45ad0fa9c22474081ed0d253a7d63', bigImg:'/index/showImage?pathName=test&imgName=5dbc68f5ab19c9bd74dc21b15fd360e86151c65a' },
            { img: '/index/showImage?pathName=test&imgName=2cc06f1bffa45ad0fa9c22474081ed0d253a7d63', bigImg:'/index/showImage?pathName=test&imgName=5dbc68f5ab19c9bd74dc21b15fd360e86151c65a' },
            { img: '/index/showImage?pathName=test&imgName=2cc06f1bffa45ad0fa9c22474081ed0d253a7d63', bigImg:'/index/showImage?pathName=test&imgName=5dbc68f5ab19c9bd74dc21b15fd360e86151c65a' },
            { img: '/index/showImage?pathName=test&imgName=2cc06f1bffa45ad0fa9c22474081ed0d253a7d63', bigImg:'/index/showImage?pathName=test&imgName=5dbc68f5ab19c9bd74dc21b15fd360e86151c65a' },
            { img: '/index/showImage?pathName=test&imgName=2cc06f1bffa45ad0fa9c22474081ed0d253a7d63', bigImg:'/index/showImage?pathName=test&imgName=5dbc68f5ab19c9bd74dc21b15fd360e86151c65a' },
            { img: '/index/showImage?pathName=test&imgName=2cc06f1bffa45ad0fa9c22474081ed0d253a7d63', bigImg:'/index/showImage?pathName=test&imgName=5dbc68f5ab19c9bd74dc21b15fd360e86151c65a' }
        ];
        picViewer('.testabcaa');
    }
});