using System;
using System.Collections.Generic;
using System.Web.UI;
using crazypizza.Models;

namespace crazypizza
{
 public partial class Cart : Page
 {
 protected global::System.Web.UI.WebControls.GridView gvCart;

 protected void Page_Load(object sender, EventArgs e)
 {
 if (!IsPostBack)
 {
 BindCart();
 }
 }

 private void BindCart()
 {
 var cart = Session["Cart"] as List<CartItem>;
 if (cart == null) cart = new List<CartItem>();
 gvCart.DataSource = cart;
 gvCart.DataBind();
 }

 protected void btnCheckout_Click(object sender, EventArgs e)
 {
 Response.Redirect("~/Checkout.aspx");
 }
 }
}