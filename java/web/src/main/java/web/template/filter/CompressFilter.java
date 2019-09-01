package web.template.filter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.util.zip.GZIPOutputStream;
import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.WriteListener;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpServletResponseWrapper;
/**
 * 响应报文压缩过滤器
 * @author admin
 *
 */
public class CompressFilter implements Filter {

	public class GZipResponseWrapper extends HttpServletResponseWrapper {

		private GzipServletOupputStream gzipServletOupputStream;
		private PrintWriter printWriter;

		public GZipResponseWrapper(HttpServletResponse response) {
			super(response);
		}

		public ServletOutputStream getOutputStream() throws IOException {
			if (printWriter != null) {
				throw new IllegalStateException();
			}
			if (gzipServletOupputStream == null) {
				gzipServletOupputStream = new GzipServletOupputStream(this.getResponse().getOutputStream());
			}
			return gzipServletOupputStream;
		}

		public PrintWriter getWriter() throws IOException {
			if (gzipServletOupputStream != null) {
				throw new IllegalStateException();
			}
			if (printWriter == null) {
				gzipServletOupputStream = new GzipServletOupputStream(this.getResponse().getOutputStream());
				OutputStreamWriter osw = new OutputStreamWriter(gzipServletOupputStream,
						getResponse().getCharacterEncoding());
				printWriter = new PrintWriter(osw);
			}
			return printWriter;
		}

		public void setContentLength(int len) {
			super.setContentLength(len);
		}

		public GZIPOutputStream getGZIPOutputStream() {
			if (this.gzipServletOupputStream == null) {
				return null;
			}
			return this.gzipServletOupputStream.getGZIPOutputStream();
		}
	}

	public class GzipServletOupputStream extends ServletOutputStream {

		private GZIPOutputStream gzipOutputStream;

		@Override
		public void write(int b) throws IOException {
			gzipOutputStream.write(b);
		}

		public GzipServletOupputStream(ServletOutputStream servletOutPutStream) throws IOException {
			this.gzipOutputStream = new GZIPOutputStream(servletOutPutStream);
		}

		public GZIPOutputStream getGZIPOutputStream() {
			return gzipOutputStream;
		}

		@Override
		public boolean isReady() {
			return false;
		}

		@Override
		public void setWriteListener(WriteListener listener) {

		}
	}

	public void init(FilterConfig filterConfig) throws ServletException {
	}

	@Override
	public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
			throws IOException, ServletException {
		// TODO Auto-generated method stub
		HttpServletRequest req = (HttpServletRequest) request;
		HttpServletResponse res = (HttpServletResponse) response;
		// 用encodings 来判断浏览器是否有 gzip压缩的功能
		String encodings = req.getHeader("accept-encoding");
		if ((encodings != null) && (encodings.indexOf("gzip")) > -1) {
			GZipResponseWrapper gzipWrapper = new GZipResponseWrapper(res);
			chain.doFilter(request, gzipWrapper);
			// 设置响应内容编码为gzip格式
			gzipWrapper.setHeader("content-encoding", "gzip");
			GZIPOutputStream gzipOutputStream = gzipWrapper.getGZIPOutputStream();
			if (gzipOutputStream != null) {
				gzipOutputStream.finish(); 
			}
		} else {
			chain.doFilter(request, response);
		}
	}

	@Override
	public void destroy() {

	}
}
