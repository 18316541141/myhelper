package web.template.entity;

import java.util.ArrayList;
import java.util.List;

public class TreeFormNode {
	
	/**
	 * 节点d
	 */
	private String id;

    /**
     * 节点名称
     */
	private String name;

    /**
     * 子节点
     */
	private List<TreeFormNode> children;

    public TreeFormNode()
    {
        children = new ArrayList<TreeFormNode>();
    }

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public List<TreeFormNode> getChildren() {
		return children;
	}

	public void setChildren(List<TreeFormNode> children) {
		this.children = children;
	}
}