package web.template.entity.common;
public class AreaTree {
	/**
	 * 地区名称
	 */
    public String name;

    /**
     * 地区值
     */
    public String value;

    /**
     * 上级地区值
     */
    public String parentValue;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getValue() {
		return value;
	}

	public void setValue(String value) {
		this.value = value;
	}

	public String getParentValue() {
		return parentValue;
	}

	public void setParentValue(String parentValue) {
		this.parentValue = parentValue;
	}
}