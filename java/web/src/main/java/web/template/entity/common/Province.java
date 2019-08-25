package web.template.entity.common;
/**
 * 省实体类
 * @author admin
 */
public class Province {
	/**
	 * 省名称
	 */
	public String provinceName;

	/**
	 * 省值
	 */
    public int provinceID;

	public String getProvinceName() {
		return provinceName;
	}

	public void setProvinceName(String provinceName) {
		this.provinceName = provinceName;
	}

	public int getProvinceID() {
		return provinceID;
	}

	public void setProvinceID(int provinceID) {
		this.provinceID = provinceID;
	}
}