<script>
  import { config } from '$lib/config.js';

  const mailtoLink = `mailto:${config.contactInfo.supportEmail}`;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Privacy Policy</h1>
  </div>
</div>

<div class="row">
  <div class="col">
    <p>
      <strong>Effective Date:</strong> 2024-06-10
    </p>
    <ol class="lh-lg d-flex flex-column gap-2">
      <li>
        <h5>Introduction</h5>
        <p>
          Welcome to Tesla Tuition Videos! We are committed to protecting your privacy. This Privacy
          Policy explains how we collect, use, and share your personal information when you visit
          our website and use our services.
        </p>
      </li>
      <li>
        <h5>Information We Collect</h5>
        <ol>
          <li>
            <h6>Personal Information</h6>
            <p>We collect personal information that you provide directly to us, such as:</p>
            <ul>
              <li><strong>Contact Information:</strong> Email address and, optionally, name.</li>
              <li>
                <strong>Account Information:</strong> Username (same as email address) and password (hash
                only).
              </li>
              <li>
                <strong>Profile Information:</strong> Orders, purchased Content, preferences, feedback,
                and survey responses.
              </li>
            </ul>
          </li>
          <li>
            <h6>Automatically Collected Information</h6>
            <p>
              When you use our website, we automatically collect certain information about your
              device and usage, including:
            </p>
            <ul>
              <li>
                <strong>Device Information:</strong> IP address, browser type, operating system.
              </li>
              <li><strong>Usage Information:</strong> Pages visited; time spent on the site.</li>
            </ul>
          </li>
          <li>
            <h6>Cookies</h6>
            <p>
              We use authentication cookies to enhance your experience on our site and security
              cookies to protect the site and your data from malicious attacks. Cookies are small
              data files stored on your device. You can control the use of cookies through your
              browser settings. We do not use cookies for marketing or tracking purposes.
            </p>
            <p>
              For further information, please refer to our detailed <a href="cookies-policy"
                >Cookies Policy</a
              >.
            </p>
          </li>
        </ol>
      </li>
      <li>
        <h5>How We Use Your Information</h5>
        <p>We use your information to:</p>
        <ul>
          <li>
            <strong>Provide Services:</strong> Process transactions, deliver digital content, and provide
            customer support.
          </li>
          <li><strong>Personalize Experience:</strong> Customize content and recommendations.</li>
          <li>
            <strong>Improve Our Site:</strong> Analyse usage patterns to enhance our services.
          </li>
          <li>
            <strong>Communicate: </strong>Send updates, promotional materials, and respond to
            inquiries.
          </li>
          <li><strong>Security: </strong>Detect and prevent fraud and other security issues.</li>
        </ul>
      </li>
      <li>
        <h5>Sharing Your Information</h5>
        <p>We do not sell your personal information. We may share your information with:</p>
        <ul>
          <li>
            <strong>Service Providers:</strong> Third-party vendors who help us with payment processing.
          </li>
          <li>
            <strong>Legal Obligations:</strong> Authorities if required by law or to protect our rights.
          </li>
          <li>
            <strong>Business Transfers:</strong> In the event of a merger, acquisition, or sale of assets.
          </li>
        </ul>
      </li>
      <li>
        <h5>Data Security</h5>
        <p>
          We implement various security measures to protect your information, including encryption
          and access controls. However, no system is completely secure, and we cannot guarantee the
          absolute security of your data.
        </p>
      </li>
      <li>
        <h5>Your Rights and Choices</h5>
        <p>You have certain rights regarding your personal information:</p>
        <ul>
          <li><strong>Access:</strong> You can request a copy of your personal information.</li>
          <li>
            <strong>Correction:</strong> You can request corrections to inaccurate or incomplete information.
          </li>
          <li>
            <strong>Deletion:</strong> You can request the deletion of your personal information.
          </li>
          <li>
            <strong>Opt-Out:</strong> You can opt-out of receiving promotional emails by following the
            unsubscribe instructions.
          </li>
        </ul>
        <p>
          To exercise these rights, please contact us at <a href={mailtoLink}
            >{config.contactInfo.supportEmail}</a
          >.
        </p>
      </li>
      <li>
        <h5>Third-Party Links</h5>
        <p>
          Our site may contain links to third-party websites. We are not responsible for the privacy
          practices of these sites. We encourage you to review their privacy policies.
        </p>
      </li>
      <li>
        <h5>Children's Privacy</h5>
        <p>
          Our services are not intended for children under 13. We do not knowingly collect personal
          information from children under 13. If we learn we have collected such information, we
          will delete it.
        </p>
      </li>
      <li>
        <h5>International Transfers</h5>
        <p>
          Your information may be transferred to and processed in countries other than your own. We
          ensure appropriate safeguards are in place to protect your data during such transfers.
        </p>
      </li>
      <li>
        <h5>Changes to This Privacy Policy</h5>
        <p>
          We may update this Privacy Policy from time to time. We will notify you of significant
          changes by posting the new policy on our site and updating the effective date. Your
          continued use of our services after such changes indicates your acceptance of the revised
          policy.
        </p>
      </li>
      <li>
        <h5>Contact Information</h5>
        <p>
          If you have any questions about this Privacy Policy or our data practices, please contact
          us at:
        </p>
        <ul>
          <li>
            <strong>Address:</strong>
            Tesla Tuition Videos, {config.contactInfo.address}
          </li>
          <li>
            <strong>Email:</strong>
            <a href={mailtoLink}>{config.contactInfo.supportEmail}</a>
          </li>
          <li><strong>Phone:</strong> {config.contactInfo.phoneNumber}</li>
        </ul>
      </li>
    </ol>
  </div>
</div>
