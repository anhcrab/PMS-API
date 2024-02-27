<?php

/**
 * Template Name: Liên Hệ
 */
get_header(); ?>
<div class="terus-main-container">
  <section class="terus-content-section1 contact">
    <div class="container">
      <div class="row">
        <div class="col-md-6 d-flex flex-column align-items-center justify-content-center px-4 px-md-0">
          <h1 class="terus-heading_title w-100" style="max-width:100%">Liên hệ</h1>
          <h3 class="terus-heading_subtitle w-100">Cùng chúng tôi tạo nên những điều kỳ diệu</h3><img
            class="d-none d-lg-block w-100" src="https://terusvn.com/wp-content/uploads/2024/02/formimg1.png"
            alt="lien-he-terus">
          <div class="terus-contact-section2-info w-100">
            <h2>Email</h2><a href="mailto:support@terusvn.com">support@terusvn.com</a>
          </div>
        </div>
        <div class="col-md-6">
          <div id="contact-section1-form" style="background:#fff">
            <h3 class="terus-footer-service-heading">Để lại thông tin dưới đây,</h3>
            <h3 class="display-6" style="line-height:2"><span class="display-6"
                style="color:#f5dd00;font-weight:600">Terus</span> sẽ liên hệ với bạn ngay</h3>
            <form class="row g-3 needs-validation" novalidate id="contact-page-form">
              <div class="form-check col-lg-12">
                <div class="row g-3 pt-lg-4"><label for="digital-marketing" class="col-xl-4 col-lg-6"><input
                      class="form-check-input" type="radio" name="service" id="digital-marketing"
                      value="Digital Marketing" required><span class="text-black">Digital Marketing</span></label><label
                    for="design-website" class="col-xl-4 col-lg-6"><input class="form-check-input" type="radio"
                      name="service" id="design-website" value="Thiết kế Website" required><span
                      class="text-black">Thiết kế Website</span></label><label for="manage-website"
                    class="col-xl-4 col-lg-6"><input class="form-check-input" type="radio" name="service"
                      id="manage-website" value="Quản trị Website" required><span class="text-black">Quản trị
                      Website</span></label><label for="solution" class="col-xl-4 col-lg-6"><input
                      class="form-check-input" type="radio" name="service" id="solution" value="Giải pháp quản lý"
                      required><span class="text-black">Giải pháp quản lý</span></label><label for="software"
                    class="col-xl-4 col-lg-6"><input class="form-check-input" type="radio" name="service" id="software"
                      value="Thiết kế phần mềm" required><span class="text-black">Thiết kế phần mềm</span>
                    <div class="invalid-feedback">Hãy chọn dịch vụ cần tư vấn</div>
                  </label></div>
              </div>
              <div class="col-lg-12"><input type="text" class="form-control py-2" id="contact-name" placeholder="Họ tên"
                  required style="border:none;border-bottom:1px solid;box-shadow:none;border-radius:0">
                <div class="valid-feedback">Looks good!</div>
                <div class="invalid-feedback">Vui lòng nhập họ tên.</div>
              </div>
              <div class="col-lg-6"><input type="phone" class="form-control py-2" id="contact-phone"
                  placeholder="Số điện thoại" required
                  style="border:none;border-bottom:1px solid;box-shadow:none;border-radius:0">
                <div class="valid-feedback">Looks good!</div>
                <div class="invalid-feedback">Vui lòng nhập số điện thoại.</div>
              </div>
              <div class="col-lg-6"><input type="email" class="form-control py-2" id="contact-email" placeholder="Email"
                  required style="border:none;border-bottom:1px solid;box-shadow:none;border-radius:0">
                <div class="valid-feedback">Looks good!</div>
                <div class="invalid-feedback">Vui lòng nhập đúng Email.</div>
              </div>
              <div class="col-12 d-flex justify-content-center"><button class="w-100 mt-lg-3" id="contact-page-form-btn"
                  type="submit">Tư vấn ngay</button></div>
              <style>
                #contact-page-form-btn {
                  color: #fff;
                  background-color: #000;
                  padding: 15px;
                  border: none;
                  border-radius: 70px
                }
              </style>
            </form>
          </div>
        </div>
      </div>
    </div>
  </section>
  <script>const contactBtn = document.getElementById("contact-page-form-btn"), contactForm = document.getElementById("contact-page-form"); contactForm.addEventListener("submit", e => { e.preventDefault(), contactForm.checkValidity || e.stopPropagation(), contactForm.classList.add("was-validated"); let t = document.getElementById("contact-name").value, n = document.getElementById("contact-phone").value, a = document.getElementById("contact-email").value, c = document.getElementsByName("service"); if (console.log(Array.from(c).find(e => e.checked)), !t || !n || !a || void 0 == Array.from(c).find(e => e.checked)) return; let o = new FormData; o.append("name", t), o.append("phone", n), o.append("email", a), o.append("service", Array.from(c).find(e => e.checked).value), contactBtn.innerHTML = '<div class="spinner-border" style="width:24px; height:24px" role="status"><span class="visually-hidden">Loading...</span></div>', fetch("https://terusvn.com/wp-json/api/contacts", { method: "POST", body: o }).then(() => { contactBtn.innerHTML = '<i class="bi bi-send-check-fill text-white" style="font-size:24px"></i>', setTimeout(() => { location.reload() }, 800) }) }, !1);</script>
  <style>
    .form-check-input:checked {
      background-color: #cdcdcd;
      border-color: #cdcdcd
    }

    .form-check-input:focus {
      box-shadow: 0 0 0 .25rem #eee
    }
  </style>
  <div class="terus-section-blur-wrapper">
    <section class="terus-contact-section2">
      <div class="container">
        <div class="row terus-contact-section2-wrap">
          <div class="col-lg-3">
            <div class="terus-contact-section2-heading">Bạn đã sẵn sàng chia sẻ cùng chúng tôi?</div>
          </div>
          <div class="col-lg-8 terus-contact-section2-info-container">
            <div class="row">
              <h2 class="terus-contact-page-title">Thông tin liên hệ</h2>
            </div>
            <div class="row">
              <div class="col-lg-4 col-md-4">
                <div class="terus-contact-section2-info">
                  <h2>Email</h2><a href="mailto:support@terusvn.com">support@terusvn.com</a>
                </div>
              </div>
              <div class="col-lg-8 col-md-8">
                <div class="terus-contact-section2-info">
                  <h2>Địa chỉ</h2>
                  <div style="color:#000">78/24 Đường số 7, Bình Hưng Hòa, Bình Tân, HCM, Việt Nam.</div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-lg-4 col-md-4">
                <div class="terus-contact-section2-info">
                  <h2>Website</h2><a href="/">terusvn.com</a>
                </div>
              </div>
              <div class="col-lg-8 col-md-8">
                <div class="terus-contact-section2-info">
                  <h2>Social</h2>
                  <div class="terus-contact-section2-info-social-container">
                    <div class="row terus-contact-section2-info-social"><a target="_blank"
                        href="https://www.facebook.com/Terusvn" class="col-lg-3 col-md-3 col-sm-3">Facebook</a><a
                        target="_blank" href="https://www.instagram.com/terus_media/"
                        class="col-lg-3 col-md-3 col-sm-3">Instagram</a><a target="_blank"
                        href="https://www.pinterest.com/terusmedia/" class="col-lg-3 col-md-3 col-sm-3">Pinterest</a><a
                        href="mailto:support@terusvn.com" class="col-lg-3 col-md-3 col-sm-3">Email</a></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <section>
      <?php the_content(); ?>
    </section>
  </div>
</div>
<?php get_footer(); ?>